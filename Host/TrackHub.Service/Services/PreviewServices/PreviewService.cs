using TrackHub.Service.Services.PreviewServices.Dto;
using TrackHub.Service.Services.PreviewServices.Models;

namespace TrackHub.Service.Services.PreviewServices;

internal class PreviewService : IPreviewService
{
    public async Task<PreviewStateModel> PreviewExerciseAsync(string previewText, CancellationToken cancellationToken)
    {
        var result = new PreviewStateModel();
        DateOnly? playDate;

        string[] lines = previewText
            .TrimStart()
            .Split(["\r\n", "\n", "\r"], StringSplitOptions.None);

        var dateIssue = TryParseMarkedDate(lines[0], out playDate);
        if (dateIssue != null)
            result.ValidationIssues.Add(dateIssue);
        else
            result.PlayDate = playDate;
    
        for (int i = 1; i < lines.Length; i++)
        {
            PreviewRecordModel? record;
            var issues = TryParseExerciseLine(lines[i], i, out record);

            if (issues != null && issues.Any())
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

    public IList<ValidationIssue>? TryParseExerciseLine(string input, int lineNumber, out PreviewRecordModel? record)
    {
        record = default;

        PracticeLine practiceLine;
        if (!TryParsePracticeLine(input, out practiceLine))
        {
            return new List<ValidationIssue>()
            {
                new ValidationIssue()
                {
                    FieldName = "Record",
                    LineNumber = lineNumber,
                    ErrorReason = "Error while parsing a record line"
                }
            };
        }

        record = new PreviewRecordModel()
        {
            Name = practiceLine.Song,
            PlayDuration = practiceLine.Minutes,
            Author = practiceLine.Band,
            IsRecorded = practiceLine.IsStarred,
            WarmupSongs = practiceLine.WarmupSongs,
            PlayType = Domain.Enums.PlayType.Both,
            RecordType = Domain.Enums.RecordType.Song,            
        };

        string keyword = practiceLine.Keyword.ToLower();
        if (keyword == "warmup")
        {
            record.RecordType = Domain.Enums.RecordType.Warmup;
            record.Instrument = Domain.Enums.Instrument.Guitar;
        }
        else
        {
            record.RecordType = Domain.Enums.RecordType.Song;
            record.Instrument = keyword == "guitar" ?
                Domain.Enums.Instrument.Guitar : Domain.Enums.Instrument.Bass;            
        }

        return null;
    }

    public ValidationIssue? TryParseMarkedDate(string input, out DateOnly? dt)
    {
        dt = default;

        string fieldName = "Play Date";
        ValidationIssue issue = new ValidationIssue()
        {
            FieldName = fieldName,
            LineNumber = 0,
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

    private bool TryParsePracticeLine(string input, out PracticeLine result)
    {
        result = null;

        var warmupMatch = PreviewTemplates.WarmupPattern.Match(input);
        if (warmupMatch.Success)
        {
            result = new PracticeLine
            {
                Index = int.Parse(warmupMatch.Groups["index"].Value),
                Minutes = int.Parse(warmupMatch.Groups["minutes"].Value),
                Keyword = warmupMatch.Groups["keyword"].Value.Trim(),
            };

            var songsRaw = warmupMatch.Groups["songs"].Value;
            result.WarmupSongs = songsRaw
                .Split(',')
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .ToList();

            return true;
        }

        var match = PreviewTemplates.RegularPattern.Match(input);
        if (!match.Success)
            return false;

        result = new PracticeLine
        {
            Index = int.Parse(match.Groups["index"].Value),
            Minutes = int.Parse(match.Groups["minutes"].Value),
            Keyword = match.Groups["keyword"].Value.Trim(),
            Band = match.Groups["band"].Value.Trim(),
            Song = match.Groups["song"].Value.Trim(),
            SoloText = match.Groups["solo"].Success ? match.Groups["solo"].Value.Trim() : null,
            IsStarred = match.Groups["star"].Success
        };

        return true;
    }
}
