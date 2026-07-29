namespace TrackHub.Service.Exercises.Models;

public record UpdateRecordModel : CreateRecordModel
{
    public string? RecordId { get; set; }
}
