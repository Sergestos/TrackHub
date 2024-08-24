namespace TrackHub.Web.Models;

public class RecordListItem
{
    public required string RecordType { get; set; }

    public required string Name { get; set; }

    public required string Author { get; set; }

    public required int Duration { get; set; }    
}
