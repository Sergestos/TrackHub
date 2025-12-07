using TrackHub.Domain.Entities;
using TrackHub.Service.Services.PreviewServices.Models;

namespace TrackHub.Service.Services.PreviewServices;

internal class PreviewService : IPreviewService
{
    public async Task<PreviewValidationModel> PreviewExerciseAsync(string previewText, CancellationToken cancellationToken)
    {
        var result = new PreviewValidationModel();
        DateOnly? playDate;

        string[] lines = previewText
            .TrimStart()
            .Split(["\r\n", "\n", "\r"], StringSplitOptions.None);

        var dateIssue = TryParseMarkedDate(lines[0], out playDate);
        if (dateIssue != null)
            result.ValidationIssues.Add(dateIssue);
        else
            result.PlayDate = playDate;
    

        foreach (var item in lines.Skip(1))
        {
            Record? record;
            var issues = TryParseExerciseLine(item, out record);

            if (issues.Any())
            {
                foreach (var issue in issues)
                    result.ValidationIssues.Add(issue);
            }
            else
            {
                result.Records.Add(record!);
            }
        }

        result.IsValid = !result.ValidationIssues.Any();

        return result;
    }

    public IList<ValidationIssue> TryParseExerciseLine(string input, out Record? record)
    {
        record = null;
        return null;
    }

    public ValidationIssue? TryParseMarkedDate(string input, out DateOnly? dt)
    {
        dt = default;

        string fieldName = "Play Date";
        ValidationIssue issue = new ValidationIssue()
        {
            FieldName = fieldName,
            LineNumber = 1,
        };

        string clean = string.Concat(input.Where(c => !char.IsWhiteSpace(c)));
        if (!clean.StartsWith("--") || !clean.EndsWith("--"))
        {
            issue.ErrorReason = "Play Date should start and end with --";
            return issue;
        }

        string inner = clean.Substring(2, clean.Length - 4);
        var parts = inner.Split('.');

        if (parts.Length != 3)
        {
            issue.ErrorReason = "Play Date should have 3 parts (DD/MM/YYYY) --";
            return issue;
        }

        if (!int.TryParse(parts[0], out int day))
        {
            issue.ErrorReason = "Could not validate days (DD format required)";
            return issue;
        }
        if (!int.TryParse(parts[1], out int month))
        {
            issue.ErrorReason = "Could not validate months (MM format required)";
            return issue;
        };
        if (!int.TryParse(parts[2], out int year))
        {
            issue.ErrorReason = "Could not validate year (YYYY format required)";
            return issue;
        };

        if (year < 100)
            year += 2000;

        try
        {
            dt = new DateOnly(year, month, day);
            return null;
        }
        catch
        {
            return issue;
        }
    }
}
