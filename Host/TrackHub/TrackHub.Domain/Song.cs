using TrackHub.Domain.Enums;

namespace TrackHub.Domain;

public class Song
{
    public int? Id { get; set; }

    public required string Name { get; set; }

    public required ItemSourceEnum Source { get; set; }
}
