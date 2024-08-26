namespace TrackHub.Service.ExerciseServices.Models;

public record UpdateRecordModel : CreateRecordModel
{
    public required string RecordId { get; set; }
}
