using TrackHub.Domain.Enums;

namespace TrackHub.Service.Services.PreviewServices.Models;

public class PreviewRecordModel
{
    public RecordType RecordType { get; set; }

    public Instrument Instrument { get; set; }

    public PlayType PlayType { get; set; }

    public required int PlayDuration { get; set; }

    public required string Name { get; set; }

    public string? Author { get; set; }

    public int? BitsPerMinute { get; set; }

    public bool IsRecorded { get; set; }

    public List<string> WarmupSongs { get; set; } = new();
}
