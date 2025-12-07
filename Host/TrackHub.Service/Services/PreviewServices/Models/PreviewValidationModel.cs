namespace TrackHub.Service.Services.PreviewServices.Models;

public record PreviewValidationModel
{
    public bool IsValid { get; set; }

    public DateOnly? PlayDate { get; set; }

    public IList<PreviewRecordModel> Records { get; set; } = new List<PreviewRecordModel>();

    public IList<ValidationIssue> ValidationIssues {get; set; } = new List<ValidationIssue>();
}

public class ValidationIssue 
{
    public required string FieldName { get; set; }

    public required int LineNumber { get; set; }

    public string? ErrorReason { get; set; }
}