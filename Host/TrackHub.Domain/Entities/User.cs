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

    [JsonProperty("login_session")]
    public LoginSession? LoginSession { get; set; }

    [JsonProperty("last_play_date")]
    public DateTimeOffset? LastPlayDate { get; set; }

    [JsonProperty("first_play_date")]
    public DateTimeOffset? FirstPlayDate { get; set; }    

    [JsonProperty("ordered_by_duration_played_songs")]
    public List<UserSongItem>? UserSongItem { get; set; }
}

public class LoginSession
{
    [JsonProperty("user_id")]
    public required string UserId { get; set; }

    [JsonProperty("session_id")]
    public required string SessionId { get; set; }

    [JsonProperty("created_at")]
    public required DateTimeOffset CreatedAt { get; set; }

    [JsonProperty("expires_at")]
    public required DateTimeOffset ExpiresAt { get; set; }
}

public class UserSongItem
{
    public required string UserId { get; set; }

    public required string SongName { get; set; }

    public required int DurationPosition { get; set; }
}