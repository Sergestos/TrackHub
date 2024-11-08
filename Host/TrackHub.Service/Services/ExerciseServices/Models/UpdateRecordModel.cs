namespace TrackHub.Service.Services.ExerciseServices.Models;

public record UpdateRecordModel : CreateRecordModel
{
    public string? RecordId { get; set; }
}
