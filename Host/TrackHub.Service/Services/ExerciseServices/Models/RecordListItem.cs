using TrackHub.Domain.Enums;

namespace TrackHub.Service.Services.ExerciseServices.Models;

public class RecordListItem
{
    public required RecordType RecordType { get; set; }

    public required int Duration { get; set; }

    public string? Name { get; set; }

    public string? Author { get; set; }

    public IList<string>? WarmupSongs { get; set; }
}
