using Newtonsoft.Json;

namespace TrackHub.Domain.Entities;

public class Exercise
{
    [JsonProperty("type")]
    public string EntityType { get; } = "exercise";

    [JsonProperty("id")]
    public required string ExerciseId { get; set; }

    [JsonProperty("user_id")]
    public required string UserId { get; set; }

    [JsonProperty("records")]
    public required Record[] Records { get; set; }

    [JsonProperty("play_date")]
    public required PlayDate PlayDate { get; set; }

    public DateTime GetPlayDateAsDateTime()
    {
        return new DateTime(PlayDate.Year, PlayDate.Month, PlayDate.Day);
    }
}
