using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TrackHub.Domain.Enums;

namespace TrackHub.Domain.Entities;

public record Record
{
    [JsonProperty("record_id")]
    public required string RecordId { get; set; }

    [JsonProperty("instrument")]
    [JsonConverter(typeof(StringEnumConverter))]
    public required Instrument Instrument { get; set; } = Instrument.Guitar;

    [JsonProperty("record_type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public required RecordType RecordType { get; set; }

    [JsonProperty("play_type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public required PlayType PlayType { get; set; }

    [JsonProperty("play_duration")]
    public required int PlayDuration { get; set; }

    [JsonProperty("name")]
    public required string Name { get; set; }

    [JsonProperty("author")]
    public string? Author { get; set; }

    [JsonProperty("is_publicly_searchable")]
    public bool IsPubliclySearchable { get; set; }

    [JsonProperty("bits_per_minute")]
    public int? BitsPerMinute { get; set; }

    [JsonProperty("is_recorded")]
    public bool IsRecorded { get; set; }
}
