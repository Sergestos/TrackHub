namespace TrackHub.Contract.Inputs;

public class ExerciseCreateModel
{
    public required string UserId { get; set; }

    public required RecordCreateModel[] Records { get; set; }

    public required DateTimeOffset PlayDate { get; set; }
}
