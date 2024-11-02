using AutoMapper;
using System.Reflection;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Repositories;
using TrackHub.Service.ExerciseServices.Models;

namespace TrackHub.Service.ExerciseServices;

internal class ExerciseService : IExerciseService
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMapper _mapper;

    public ExerciseService(IExerciseRepository exerciseRepository, IMapper mapper)
    {
        _exerciseRepository = exerciseRepository;
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

        return await _exerciseRepository.UpsertExerciseAsync(newExercise, cancellationToken);
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
}
