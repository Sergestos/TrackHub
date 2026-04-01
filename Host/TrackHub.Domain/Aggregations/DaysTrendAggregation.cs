using Newtonsoft.Json;

namespace TrackHub.Domain.Aggregations;

public class DaysTrendAggregation
{
    [JsonProperty("id")]
    public required string AggregationId { get; set; }

    [JsonProperty("build_date")]
    public required DateTime BuildDate { get; set; }

    [JsonProperty("bars")]
    public IList<DayTrendBar>? DaysTrendBarList { get; set; } = new List<DayTrendBar>();
}

public class DayTrendBar
{
    [JsonProperty("play_date")]
    public required DateTime PlayDate { get; set; }

    [JsonProperty("rhythm_aggregation")]
    public int TotalPlayedRhythmDuration { get; set; }

    [JsonProperty("solo_aggregation")]
    public int TotalPlayedSoloDuration { get; set; }

    [JsonProperty("both_aggregation")]
    public int TotalPlayedBothDuration { get; set; }

    [JsonProperty("warmup_aggregation")]
    public int? TotalWarmupDuration { get; set; }

    [JsonProperty("song_aggregation")]
    public int? TotalSongDuration { get; set; }

    [JsonProperty("improvisation_aggregation")]
    public int? TotalImprovisationDuration { get; set; }

    [JsonProperty("exercise_aggregation")]
    public int? TotalPracticalExerciseDuration { get; set; }

    [JsonProperty("composing_aggregation")]
    public int? TotalComposingDuration { get; set; }
}
