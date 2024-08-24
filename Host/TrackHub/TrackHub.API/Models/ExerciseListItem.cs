namespace TrackHub.Web.Models;

public class ExerciseListItem
{
    public required string ExerciseId { get; set; }

    public required DateTime PlayDate { get; set; }

    public required RecordListItem[] Records { get; set; }
}
