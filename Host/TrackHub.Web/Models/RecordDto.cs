using TrackHub.Domain.Enums;

namespace TrackHub.Web.Models;

public class RecordDto
{
    public required string RecordId { get; set; }

    public required RecordType RecordType { get; set; }

    public required PlayType PlayType { get; set; }

    public required int PlayDuration { get; set; }

    public required string Name { get; set; }

    public required string Author { get; set; }

    public int? BitsPerMinute { get; set; }

    public bool IsRecorded { get; set; }

    public string[]? WarmupSongs { get; set; }
}
