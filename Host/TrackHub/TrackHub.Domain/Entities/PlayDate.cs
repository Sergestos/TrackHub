using Newtonsoft.Json;

namespace TrackHub.Domain.Entities;

public class PlayDate
{
    [JsonProperty("year")]
    public required int Year { get; set; }

    [JsonProperty("month")]
    public required int Month { get; set; }

    [JsonProperty("day")]
    public required int Day { get; set; }
}