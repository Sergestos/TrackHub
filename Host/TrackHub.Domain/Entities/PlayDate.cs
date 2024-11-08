using Newtonsoft.Json;

namespace TrackHub.Domain.Entities;

public class PlayDate : IComparable<PlayDate>
{
    [JsonProperty("year")]
    public required int Year { get; set; }

    [JsonProperty("month")]
    public required int Month { get; set; }

    [JsonProperty("day")]
    public required int Day { get; set; }

    public static PlayDate FormatFromDateTime(DateTime dateTime)
    {
        return new PlayDate()
        {
            Year = dateTime.Year,
            Month = dateTime.Month,
            Day = dateTime.Day
        };
    }

    public int CompareTo(PlayDate other)
    {
        if (other == null)
        {
            return 1;
        }

        int yearComparison = Year.CompareTo(other.Year);
        if (yearComparison != 0)
        {
            return yearComparison;
        }

        int monthComparison = Month.CompareTo(other.Month);
        if (monthComparison != 0)
        {
            return monthComparison;
        }

        return Day.CompareTo(other.Day);
    }
}