using Newtonsoft.Json;

namespace TrackHub.Domain.Entities;

public class Exercise
{
    [JsonProperty("exercise_id")]
    public required string ExerciseId { get; set; }

    [JsonProperty("user_id")]
    public required string UserId { get; set; }

    [JsonProperty("records")]
    public required Record[] Records { get; set; }

    [JsonProperty("play_date")]
    public required DateTimeOffset PlayDate { get; set; }
}
