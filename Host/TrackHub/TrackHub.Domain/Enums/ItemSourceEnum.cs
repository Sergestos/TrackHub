using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TrackHub.Domain.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum ItemSourceEnum
{
    DataBase,
    Cache,
    AI
}
