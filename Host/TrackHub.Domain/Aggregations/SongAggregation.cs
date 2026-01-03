using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TrackHub.Domain.Aggregations;

public class SongAggregation
{
    [JsonProperty("id")]
    public required string AggregationId { get; set; }

    [JsonProperty("type")]
    public required string Type { get; set; }

    [JsonProperty("user_id")]
    public required string UserId { get; set; }

    [JsonProperty("total_played")]
    public int TotalPlayed { get; set; }

    [JsonProperty("rhythm_played")]
    public int RhythmPlayed { get; set; }

    [JsonProperty("solo_played")]
    public int SoloPlayed { get; set; }

    [JsonProperty("both_played")]
    public int BothPlayed{ get; set; }

    [JsonProperty("times_played")]
    public int TimesPlayed { get; set; }

    [JsonProperty("author")]
    public required string Author { get; set; }

    [JsonProperty("name")]
    public required string Name { get; set; }

    [JsonProperty("date_aggregation")]
    public IList<SongsByDateAggregation> SongsByDateAggregations { get; set; } = new List<SongsByDateAggregation>();
}

public class SongsByDateAggregation
{
    [JsonPropertyName("year")]
    public int Year { get; set; } = default!;

    [JsonPropertyName("month")]
    public int Month { get; set; } = default!;

    [JsonProperty("total_played")]
    public int TimesPlayed { get; set; }

    [JsonProperty("total_duration")]
    public int TotalDuration { get; set; }

    [JsonProperty("rhythm_played")]
    public int RhythmPlayed { get; set; }

    [JsonProperty("solo_played")]
    public int SoloPlayed { get; set; }

    [JsonProperty("both_played")]
    public int BothPlayed { get; set; }
}
