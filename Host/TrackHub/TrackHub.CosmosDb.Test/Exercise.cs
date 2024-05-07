using Newtonsoft.Json;

namespace TrackHub.CosmosDb.Test;

public class Exercise
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("user_id")]
    public string UserId { get; set; }

    public Song[] Songs { get; set; }
}

public class Song
{
    public string SongId { get; set; }

    public string SongName { get; set; }
}