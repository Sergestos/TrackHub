using Newtonsoft.Json;

namespace TrackHub.Domain.Aggregations;

public class ExerciseAggregation 
{
    [JsonProperty("id")]
    public required string Id { get; set; }

    [JsonProperty("type")]
    public required string Type { get; set; }

    [JsonProperty("userId")]
    public required string UserId { get; set; }

    [JsonProperty("play_date")]
    public required DateTime PlayDate { get; set; }

    [JsonProperty("total_played")]
    public int TotalPlayed { get; set; }

    [JsonProperty("warmup_aggregation")]
    public ByRecordTypeAggregation? WarmupAggregation { get; set; }

    [JsonProperty("song_aggregation")]
    public ByRecordTypeAggregation? SongAggregation { get; set; }

    [JsonProperty("improvisation_aggregation")]
    public ByRecordTypeAggregation? ImprovisationAggregation { get; set; }

    [JsonProperty("exercise_aggregation")]
    public ByRecordTypeAggregation? PracticalExerciseAggregation { get; set; }

    [JsonProperty("composing_aggregation")]
    public ByRecordTypeAggregation? ComposingAggregation { get; set; }

    [JsonProperty("rhythm_aggregation")]
    public ByPlayTypeAggregation? RhythmAggregation { get; set; }

    [JsonProperty("solo_aggregation")]
    public ByPlayTypeAggregation? SoloAggregation { get; set; }

    [JsonProperty("both_aggregation")]
    public ByPlayTypeAggregation? BothAggregation { get; set; }
}

public class ByRecordTypeAggregation
{
    [JsonProperty("record_type_name")]
    public string RecordTypeName { get; set; }

    [JsonProperty("times_played")]
    public int TimesPlayed { get; set; }

    [JsonProperty("total_played")]
    public int TotalPlayed { get; set; }

    public ByRecordTypeAggregation(string recordTypeName, int timesPlayed, int totalPlayed)
    {
        RecordTypeName = recordTypeName;
        TimesPlayed = timesPlayed;
        TotalPlayed = totalPlayed;
    }
}

public class ByPlayTypeAggregation
{
    [JsonProperty("play_type_name")]
    public required string PlayTypeName { get; set; }

    [JsonProperty("times_played")]
    public int TimesPlayed { get; set; }

    [JsonProperty("total_played")]
    public int TotalPlayed { get; set; }
}