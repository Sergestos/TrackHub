namespace TrackHub.Service.Exercises.Models;

public record CreateRecordModel
{
    public string? Name { get; set; }

    public string? Author { get; set; }

    public int Instrument { get; set; }

    public required int RecordType { get; set; }

    public required int PlayType { get; set; }

    public required int PlayDuration { get; set; }

    public int? BitsPerMinute { get; set; }

    public bool IsRecorded { get; set; } = false;

    public IList<string>? WarmupSongs { get; set; }
}
