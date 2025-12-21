using AutoMapper;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Repositories;
using TrackHub.Messaging.Aggregations;
using TrackHub.Service.Aggregation.Services;
using TrackHub.Service.Services.ExerciseServices.Models;

namespace TrackHub.Service.Services.ExerciseServices;

internal class ExerciseService : IExerciseService
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAggregationService _aggregationService;
    private readonly IMapper _mapper;

    public ExerciseService(IExerciseRepository exerciseRepository, IUserRepository userRepository, IAggregationService aggregationService, IMapper mapper)
    {
        _exerciseRepository = exerciseRepository;
        _userRepository = userRepository;
        _aggregationService = aggregationService;
        _mapper = mapper;
    }

    public async Task<Exercise> CreateExerciseAsync(CreateExerciseModel exerciseModel, string userId, CancellationToken cancellationToken)
    {
        User user = _userRepository.GetUserById(userId)!;

        var exercise = _exerciseRepository.GetExerciseByDate(DateOnly.FromDateTime(exerciseModel.PlayDate), userId, cancellationToken);
        if (exercise != null)
            throw new InvalidOperationException("Exercise already exists for this date.");

        var newExercise = new Exercise()
        {
            ExerciseId = Guid.NewGuid().ToString(),
            UserId = userId,
            PlayDate = exerciseModel.PlayDate,
            Records = exerciseModel.Records.Select(model =>
            {
                Record record = _mapper.Map<Record>(model);
                record.RecordId = Guid.NewGuid().ToString();

                return record;
            }).ToArray()
        };

        var result = await _exerciseRepository.UpsertExerciseAsync(newExercise, cancellationToken);

        await SendAggregationRequestOnCreateAsync(newExercise.Records, newExercise.PlayDate, userId, cancellationToken);

        if (TryRecalculatePlayDatesOnCreate(user!, result))
            await _userRepository.UpsertAsync(user, cancellationToken);

        return result;
    }

    public async Task<Exercise> UpdateExerciseAsync(UpdateExerciseModel exerciseModel, string userId, CancellationToken cancellationToken)
    {
        var exercise = await _exerciseRepository.GetExerciseByIdAsync(exerciseModel.ExerciseId, userId, cancellationToken);
        if (exercise == null)
            throw new InvalidOperationException("Exercise is not found.");

        var oldRecords = exercise.Records;    

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

        await SendAggregationRequestOnUpdateAsync(
            exercise.Records,
            oldRecords,
            userId, exercise.PlayDate, cancellationToken);

        return result;
    }

    public async Task DeleteExerciseAsync(string exerciseId, string userId, CancellationToken cancellationToken)
    {
        User user = _userRepository.GetUserById(userId)!;

        var exercise = await _exerciseRepository.GetExerciseByIdAsync(exerciseId, userId, cancellationToken);
        if (exercise == null)
            throw new InvalidOperationException("Exercise is not found.");

        await _exerciseRepository.DeleteExerciseAsync(exerciseId, userId, cancellationToken);

        if (await TryRecalculatePlayDatesOnDeleteAsync(user, exercise.PlayDate, cancellationToken))
            await _userRepository.UpsertAsync(user, cancellationToken);
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

    private bool TryRecalculatePlayDatesOnCreate(User user, Exercise addedExercise)
    {
        DateTimeOffset exercisePlayDate = addedExercise.PlayDate;

        if (user.FirstPlayDate != null && user.FirstPlayDate <= exercisePlayDate &&
            user.LastPlayDate != null && user.LastPlayDate >= exercisePlayDate)
        {
            return false;
        }

        if (user.FirstPlayDate == null || user.FirstPlayDate > exercisePlayDate)
            user.FirstPlayDate = exercisePlayDate;

        if (user.LastPlayDate == null || user.LastPlayDate < exercisePlayDate)
            user.LastPlayDate = exercisePlayDate;

        return true;
    }

    private async Task<bool> TryRecalculatePlayDatesOnDeleteAsync(User user, DateTime removedExercisePlayDate, CancellationToken cancellation)
    {
        var newestExercise = await _exerciseRepository.FindNewestExerciseAsync(user.UserId, cancellation);
        if (newestExercise != null)
        {
            var newestExercisePlayDate = newestExercise.PlayDate;
            if (newestExercise.PlayDate < removedExercisePlayDate)
            {
                user.LastPlayDate = newestExercisePlayDate;
                return true;
            }
        }

        var oldestExercise = await _exerciseRepository.FindOldestExerciseAsync(user.UserId, cancellation);
        if (oldestExercise != null)
        {
            var oldestExercisePlayDate = oldestExercise.PlayDate;
            if (oldestExercise.PlayDate > removedExercisePlayDate)
            {
                user.FirstPlayDate = oldestExercisePlayDate;
                return true;
            }
        }

        return false;
    }

    private async Task SendAggregationRequestOnCreateAsync(Record[] records, DateTime playDate, string userId, CancellationToken cancellationToken)
    {
        var aggregationMessage = new AggregationEventMessage()
        {
            EventDate = DateTime.UtcNow,
            PlayDate = playDate,
            UserId = userId,
            NewRecords = _mapper.Map<AggregationRecord[]>(records),
            OldRecords = null,
        };

        await _aggregationService.SendAggregationAsync(aggregationMessage, cancellationToken);
    }

    private async Task SendAggregationRequestOnUpdateAsync(
        Record[] newRecords,
        Record[] oldRecords,
        string userId,
        DateTime playDate,
        CancellationToken cancellationToken)
    {
        var aggregationMessage = new AggregationEventMessage()
        {
            EventDate = DateTime.UtcNow,
            PlayDate = playDate,
            UserId = userId,
            NewRecords = _mapper.Map<AggregationRecord[]>(newRecords),
            OldRecords = _mapper.Map<AggregationRecord[]>(oldRecords)
        };

        await _aggregationService.SendAggregationAsync(aggregationMessage, cancellationToken);
    }
}
