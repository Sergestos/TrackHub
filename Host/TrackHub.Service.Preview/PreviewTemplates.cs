using System.Text.RegularExpressions;

namespace TrackHub.Application.Service.Preview;

internal class PreviewTemplates
{
    internal static Regex RegularPattern = new Regex(
        @"^\s*
          (?<index>\d+)
          [\)\.]
          \s*
          (?<minutes>\d+)
          \s*min
          \s*:\s*
          (?<keyword>[^-]+?)
          \s*-\s*
          (?<band>[^-]+?)
          \s*-\s*
          (?<song>.+?)
          (?:\s+(?<soloPlus>\+)?\s*(?<solo>solo))?
          \s*
          (?<star>\(\*\))?
          \s*$
        ",
        RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);


    internal static Regex WarmupPattern = new Regex(
            @"^\s*
          (?<index>\d+)
          [\)\.]
          \s*
          (?<minutes>\d+)
          \s*min
          \s*:\s*
          (?<keyword>warm\s*up|warmp\s*up)
          \s*-\s*
          (?<songs>.+?)
          \s*$
        ",
        RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
}