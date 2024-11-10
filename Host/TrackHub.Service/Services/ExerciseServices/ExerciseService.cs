using AutoMapper;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Repositories;
using TrackHub.Service.Services.ExerciseServices.Models;

namespace TrackHub.Service.Services.ExerciseServices;

internal class ExerciseService : IExerciseService
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public ExerciseService(IExerciseRepository exerciseRepository, IUserRepository userRepository, IMapper mapper)
    {
        _exerciseRepository = exerciseRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Exercise> CreateExerciseAsync(CreateExerciseModel exerciseModel, string userId, CancellationToken cancellationToken)
    {
        var exercise = _exerciseRepository.GetExerciseByDate(DateOnly.FromDateTime(exerciseModel.PlayDate), userId, cancellationToken);
        if (exercise != null)
            throw new InvalidOperationException("Exercise already exists for this date.");

        var newExercise = new Exercise()
        {
            ExerciseId = Guid.NewGuid().ToString(),
            UserId = userId,
            PlayDate = PlayDate.FormatFromDateTime(exerciseModel.PlayDate),
            Records = exerciseModel.Records.Select(model =>
            {
                Record record = _mapper.Map<Record>(model);
                record.RecordId = Guid.NewGuid().ToString();

                return record;
            }).ToArray()
        };

        var result = await _exerciseRepository.UpsertExerciseAsync(newExercise, cancellationToken);

        User user = _userRepository.GetUserById(userId)!;
        if (user.LastPlayDate != null && user.FirstPlayDate != null)
        {
            ReassignPlayDatesOnCreate(user!, result);
            await _userRepository.UpsertAsync(user, cancellationToken);
        }            

        return result;
    }

    public async Task<Exercise> UpdateExerciseAsync(UpdateExerciseModel exerciseModel, string userId, CancellationToken cancellationToken)
    {
        var exercise = await _exerciseRepository.GetExerciseByIdAsync(exerciseModel.ExerciseId, userId, cancellationToken);
        if (exercise == null)
            throw new InvalidOperationException("Exercise is not found.");

        IEnumerable<UpdateRecordModel> newRecords = exerciseModel.Records.Where(x => string.IsNullOrWhiteSpace(x.RecordId));
        IEnumerable<UpdateRecordModel> existingRecords = exerciseModel.Records.Where(x => !string.IsNullOrWhiteSpace(x.RecordId));

        exercise.Records = _mapper.Map<Record[]>(existingRecords)
            .Union(newRecords.Select(x =>
            {
                Record record = _mapper.Map<Record>(x);
                record.RecordId = Guid.NewGuid().ToString();

                return record;
            }))
            .ToArray();

        var result = await _exerciseRepository.UpsertExerciseAsync(exercise, cancellationToken);

        return result;
    }

    public async Task DeleteExerciseAsync(string exerciseId, string userId, CancellationToken cancellationToken)
    {
        var exercise = await _exerciseRepository.GetExerciseByIdAsync(exerciseId, userId, cancellationToken);
        if (exercise == null)
            throw new InvalidOperationException("Exercise is not found.");

        await _exerciseRepository.DeleteExerciseAsync(exerciseId, userId, cancellationToken);

        User user = _userRepository.GetUserById(userId)!;
        if (user.LastPlayDate != null && user.FirstPlayDate != null)
        {
            await ReassignPlayDatesOnDeleteAsync(user, exercise, cancellationToken);
            await _userRepository.UpsertAsync(user, cancellationToken);
        }
    }

    public async Task<Exercise> DeleteRecordsAsync(string exerciseId, string[] recordIds, string userId, CancellationToken cancellationToken)
    {
        var exercise = await _exerciseRepository.GetExerciseByIdAsync(exerciseId, userId, cancellationToken);
        if (exercise == null)
            throw new InvalidOperationException("Exercise is not found.");

        exercise.Records = exercise.Records.Where(x => !recordIds.Contains(x.RecordId)).ToArray();
        var result = await _exerciseRepository.UpsertExerciseAsync(exercise, cancellationToken);

        return result;
    }

    private void ReassignPlayDatesOnCreate(User user, Exercise addedExercise)
    {
        DateTimeOffset exercisePlayDate = addedExercise.PlayDate.ToDateTime();

        if (user.FirstPlayDate == null || user.LastPlayDate == null)
        {
            user.FirstPlayDate = exercisePlayDate;
            user.LastPlayDate = exercisePlayDate;

            return;
        }

        if (exercisePlayDate < user.FirstPlayDate)
        {
            user.FirstPlayDate = exercisePlayDate;
            return;
        }

        if (exercisePlayDate > user.LastPlayDate)
        {
            user.LastPlayDate = exercisePlayDate;
            return;
        }
    }

    private async Task ReassignPlayDatesOnDeleteAsync(User user, Exercise removedExercise, CancellationToken cancellation)
    {
        var removedExercisePlayDate = removedExercise.PlayDate.ToDateTime();

        var newestExercise = await _exerciseRepository.FindNewestExercise(user.UserId, cancellation);
        if (newestExercise != null)
        {
            var newestExercisePlayDate = newestExercise.PlayDate.ToDateTime();
            if (newestExercise.PlayDate.ToDateTime() < removedExercisePlayDate)
            {
                user.LastPlayDate = newestExercisePlayDate;
                return;
            }
        }

        var oldestExercise = await _exerciseRepository.FindOldestExercise(user.UserId, cancellation);
        if (oldestExercise != null)
        {
            var oldestExercisePlayDate = oldestExercise.PlayDate.ToDateTime();
            if (oldestExercise.PlayDate.ToDateTime() > removedExercisePlayDate)
            {
                user.FirstPlayDate = oldestExercisePlayDate;
                return;
            }
        }
    }
}
