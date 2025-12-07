using System.Text.RegularExpressions;

namespace TrackHub.Service.Services.PreviewServices;

internal class PreviewTemplates
{
    internal static Regex RegularPattern = new Regex(
        @"^\s*
          (?<index>\d+)
          \)
          \s*
          (?<minutes>\d+)
          \s*min
          \s*:\s*
          (?<keyword>[^-]+?)
          \s*-\s*
          (?<band>[^-]+?)
          \s*-\s*
          (?<song>.+?)
          (?:\s+(?<solo>\+?\s*solo))?
          \s*
          (?<star>\(\*\))?
          \s*$
        ",
        RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);


    internal static Regex WarmupPattern = new Regex(
        @"^\s*
          (?<index>\d+)
          \)
          \s*
          (?<minutes>\d+)
          \s*min
          \s*:\s*
          (?<keyword>warmup)
          \s*-\s*
          (?<songs>.+?)
          \s*$
        ",
        RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
}
