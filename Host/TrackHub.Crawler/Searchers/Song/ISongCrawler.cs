namespace TrackHub.Crawler.Searchers.Song;

public interface ISongCrawler
{
    IEnumerable<string> Search(string songName);

    IEnumerable<string> Search(string authorName, string songName);
}
