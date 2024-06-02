using Newtonsoft.Json;

namespace TrackHub.Domain.Entities;

public class Password
{
    [JsonProperty("hash")]
    public required string Hash { get; set; }

    [JsonProperty("salt")]
    public required string Salt { get; set; }
}
