using TrackHub.Service.Services.PreviewServices.Models;

namespace TrackHub.Service.Services.PreviewServices;

internal class PreviewService : IPreviewService
{
    public async Task<PreviewValidationModel> PreviewExerciseAsync(string previewText, CancellationToken cancellationToken)
    {
        var result = new PreviewValidationModel();     
        DateOnly playDate;

        string[] lines = previewText
            .TrimStart()
            .Split(["\r\n", "\n", "\r"], StringSplitOptions.None);

        if (!TryParseMarkedDate(lines[0], out playDate)) {
            result.ValidationIssues.Add(new ValidationIssue()
            {
                FieldName = "play date",
                LineNumber = 1,
                ErrorReason = "Play date line has errors"
            });
        }

        result.IsValid = !result.ValidationIssues.Any();
        result.PlayDate = playDate;
        
        return result;
    }

    public bool TryParseMarkedDate(string input, out DateOnly dt)
    {
        dt = default;

        string clean = string.Concat(input.Where(c => !char.IsWhiteSpace(c)));

        if (!clean.StartsWith("--") || !clean.EndsWith("--"))
            return false;

        string inner = clean.Substring(2, clean.Length - 4);

        var parts = inner.Split('.');

        if (parts.Length != 3)
            return false;

        if (!int.TryParse(parts[0], out int day)) return false;
        if (!int.TryParse(parts[1], out int month)) return false;
        if (!int.TryParse(parts[2], out int year)) return false;

        if (year < 100)
            year += 2000;

        try
        {
            dt = new DateOnly(year, month, day);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
