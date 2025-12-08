namespace TrackHub.Service.Services.PreviewServices.Dto;

internal class PracticeLine
{
    public int Index { get; set; }

    public int Minutes { get; set; }

    public string Keyword { get; set; }

    public string Band { get; set; }

    public string Song { get; set; }

    public string SoloText { get; set; }

    public bool IsStarred { get; set; }

    public bool IsWarmup =>
        !string.IsNullOrEmpty(Keyword) &&
        Keyword.Equals("warmup", StringComparison.OrdinalIgnoreCase);

    public List<string> WarmupSongs { get; set; } = new();
}
