using TrackHub.Domain.Entities;
using TrackHub.Domain.Enums;
using TrackHub.Domain.Repositories;
using TrackHub.Service.ExerciseServices.Models;

namespace TrackHub.Service.ExerciseServices;

internal class ExerciseService : IExerciseService
{
    private readonly IUserRepository _userRepository;
    private readonly IExerciseRepository _exerciseRepository;

    public ExerciseService(IUserRepository userRepository, IExerciseRepository exerciseRepository)
    {
        _userRepository = userRepository;
        _exerciseRepository = exerciseRepository;
    }

    public async Task<Exercise> CreateExercise(CreateExerciseModel exerciseModel, string userEmail, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetUserByEmail(userEmail)!;
        var exercise = _exerciseRepository.GetExerciseByDate(DateOnly.FromDateTime(exerciseModel.PlayDate), user.UserId, cancellationToken);
        if (exercise != null)
            throw new InvalidOperationException("Exercise already exists for this date.");        

        var newExercise = new Exercise()
        {
            ExerciseId = Guid.NewGuid().ToString(),            
            UserId = user.UserId,
            PlayDate = new PlayDate()
            {
                Year = exerciseModel.PlayDate.Year,
                Month = exerciseModel.PlayDate.Month,
                Day = exerciseModel.PlayDate.Day
            },
            Records = exerciseModel.Records.Select(record => new Record()
            {
                RecordType = (RecordType)Enum.Parse(typeof(RecordType), record.RecordType),
                PlayType = (PlayType)Enum.Parse(typeof(PlayType), record.PlayType),
                Name = record.Name,
                Author = record.Author,
                PlayDuration = record.Duration,
                BitsPerMinute = record.BitsPerMinute,
                IsRecorded = record.IsRecorded
            }).ToArray()
        };

        return await _exerciseRepository.UpsertExerciseAsync(newExercise, cancellationToken);
    }
}
