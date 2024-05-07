using TrackHub.Domain.Enums;

namespace TrackHub.Domain.Entities;

public record Record
{
    public RecordTypeEnum RecordType { get; set; }

    public required bool IsSearchable { get; set; }

    public required string Name { get; set; }

    public string? Author { get; set; }

    public int? BitsPerMinute { get; set; }

    public bool? IsSoloOnly { get; set; }

    public bool? IsRecorded { get; set; }
}
