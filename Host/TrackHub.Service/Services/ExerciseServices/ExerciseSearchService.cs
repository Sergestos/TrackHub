using AutoMapper;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Enums;
using TrackHub.Domain.Repositories;
using TrackHub.Service.Services.ExerciseServices.Models;

namespace TrackHub.Service.Services.ExerciseServices;

internal class ExerciseSearchService : IExerciseSearchService
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMapper _mapper;

    public ExerciseSearchService(IExerciseRepository exerciseRepository, IMapper mapper)
    {
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ExerciseListItem>> GetExercisesByDateAsync(int? year, int? month, string userId, CancellationToken cancellationToken)
    {
        int yearParams = year ?? DateTime.UtcNow.Year;
        int monthParams = month ?? DateTime.UtcNow.Month;

        IEnumerable<Exercise> exercises = await _exerciseRepository.GetExerciseListByDateAsync(yearParams, monthParams, userId, cancellationToken);
        IList<ExerciseListItem> result = new List<ExerciseListItem>();

        foreach (var exercise in exercises)
        {
            var warmupRecords = exercise.Records.Where(x => x.RecordType == RecordType.Warmup);            
            if (warmupRecords.Any())
            {
                Record squashedRecord = SquashRecords(warmupRecords.ToArray());
                exercise.Records = exercise.Records
                    .Where(x => x.RecordType != RecordType.Warmup)
                    .Append(squashedRecord)
                    .OrderBy(x => x.RecordType)
                    .ToArray();
            }            

            result.Add(_mapper.Map<ExerciseListItem>(exercise));
        }

        return result;
    }

    private Record SquashRecords(Record[] records)
    {
        int totalTime = records.Sum(x => x.PlayDuration);
        int adjustedTime = (int)(Math.Round(totalTime / 5.0) * 5);

        string adjustedName = string.Join(", ", records.Select(x => x.Name));

        return new Record()
        {
            RecordId = string.Empty,
            PlayType = PlayType.Rhythm,
            RecordType = RecordType.Warmup,
            PlayDuration = adjustedTime,
            Name = adjustedName
        };
    }
}
