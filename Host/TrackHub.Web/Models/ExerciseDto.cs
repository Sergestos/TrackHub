namespace TrackHub.Web.Models;

public class ExerciseDto
{
    public required string ExerciseId { get; set; }

    public required DateTime PlayDate { get; set; }

    public required RecordDto[] Records { get; set; }
}
