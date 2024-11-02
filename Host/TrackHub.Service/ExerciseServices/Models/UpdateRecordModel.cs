namespace TrackHub.Service.ExerciseServices.Models;

public record UpdateRecordModel : CreateRecordModel
{
    public string? RecordId { get; set; }
}
