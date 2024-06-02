using Newtonsoft.Json;
using TrackHub.Domain.Enums;

namespace TrackHub.Domain.Entities;

public record Record
{
    [JsonProperty("record_type")]
    public required  RecordTypeEnum RecordType { get; set; }

    [JsonProperty("play_type")]
    public required  PlayType PlayType { get; set; }

    [JsonProperty("is_publicly_searchable")]
    public required bool IsPubliclySearchable { get; set; }

    [JsonProperty("is_recorded")]
    public required bool IsRecorded { get; set; }

    [JsonProperty("play_duration")]
    public required int PlayDuration { get; set; }

    [JsonProperty("name")]
    public required string Name { get; set; }

    [JsonProperty("author")]
    public string? Author { get; set; }

    [JsonProperty("bits_per_minute")]
    public int? BitsPerMinute { get; set; }        
}
