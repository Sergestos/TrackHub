namespace TrackHub.Service.Exercises.Models;

public record CreateExerciseModel
{
    public required DateTime PlayDate { get; set; }

    public required IEnumerable<CreateRecordModel> Records { get; set; }
}
