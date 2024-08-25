using AutoMapper;
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

        exercise.Records = _mapper.Map<Record[]>(exerciseModel.Records.ToArray());

        var result = await _exerciseRepository.UpsertExerciseAsync(exercise, cancellationToken);

        return result;
    }
}
