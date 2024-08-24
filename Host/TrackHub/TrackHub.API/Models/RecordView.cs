namespace TrackHub.Web.Models;

public class RecordView
{
    public required string RecordId { get; set; }

    public required string RecordType { get; set; }

    public required string PlayType { get; set; }

    public required int PlayDuration { get; set; }

    public required string Name { get; set; }

    public required string Author { get; set; }

    public int? BitsPerMinute { get; set; }

    public bool IsRecorded { get; set; }
}
