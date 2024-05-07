using TrackHub.Domain.Enums;

namespace TrackHub.Contract.Inputs;

public record RecordCreateModel
{
    public RecordTypeEnum RecordType { get; set; }

    public required bool IsSearchable { get; set; }

    public required string Name { get; set; }

    public string? AuthorName { get; set; }

    public int? BitsPerMinute { get; set; }

    public bool? IsSoloOnly { get; set; }

    public bool? IsRecorded { get; set; }
}
