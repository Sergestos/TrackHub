namespace TrackHub.Web.Models;

public class ExerciseView
{
    public required string ExerciseId { get; set; }

    public required DateTime PlayDate { get; set; }

    public required RecordView[] Records { get; set; }
}
