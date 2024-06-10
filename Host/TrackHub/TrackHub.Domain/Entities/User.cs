using Newtonsoft.Json;

namespace TrackHub.Domain.Entities;

public class User
{
    [JsonProperty("type")]
    public string EntityType { get; } = "user";

    [JsonProperty("user_id")]
    public required string UserId { get; set; }    

    [JsonProperty("login")]
    public required string Login { get; set; }

    [JsonProperty("email")]
    public required string Email { get; set; }

    [JsonProperty("registration_date")]
    public required DateTimeOffset RegistrationDate { get; set; }

    [JsonProperty("last_entrance_date")]
    public DateTimeOffset? LastEntranceDate { get; set; }

    [JsonProperty("password")]
    public required Password Password { get; set; }
}
