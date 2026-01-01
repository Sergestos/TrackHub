using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace TrackHub.Domain.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum ItemSource
{
    DataBase,
    Cache,
    AI
}
