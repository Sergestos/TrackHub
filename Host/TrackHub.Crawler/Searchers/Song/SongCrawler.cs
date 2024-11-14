using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackHub.Domain.Repositories;

namespace TrackHub.Crawler.Searchers.Song;

internal class SongCrawler : ISongCrawler
{
    private readonly IExerciseRepository _exerciseRepository;

    public SongCrawler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public IEnumerable<string> Search(string songName)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> Search(string authorName, string songName)
    {
        throw new NotImplementedException();
    }
}
