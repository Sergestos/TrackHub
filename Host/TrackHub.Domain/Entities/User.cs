using Newtonsoft.Json;

namespace TrackHub.Domain.Entities;

public class User
{
    [JsonProperty("type")]
    public string EntityType { get; } = "user";

    [JsonProperty("id")]
    public required string UserId { get; set; }    

    [JsonProperty("full_name")]
    public required string FullName { get; set; }

    [JsonProperty("email")]
    public required string Email { get; set; }

    [JsonProperty("photo_url")]
    public required string PhotoUrl { get; set; }

    [JsonProperty("registration_date")]
    public required DateTimeOffset RegistrationDate { get; set; }

    [JsonProperty("last_entrance_date")]
    public DateTimeOffset? LastEntranceDate { get; set; }

    [JsonProperty("last_play_date")]
    public DateTimeOffset? LastPlayDate { get; set; }

    [JsonProperty("first_play_date")]
    public DateTimeOffset? FirstPlayDate { get; set; }
}
