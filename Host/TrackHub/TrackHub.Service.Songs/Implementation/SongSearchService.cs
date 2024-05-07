using TrackHub.Domain.Entities;
using TrackHub.Domain.Enums;

namespace TrackHub.Service.Exercises.Implementation;

internal class SongSearchService : ISongService
{
    public SongSearchService() { }

    public async Task<IEnumerable<Song>> SearchAsync(string searchText, CancellationToken cancellationToken)
    {
        var song = new Song() 
        { 
            SongName  = "hello", 
            Source = ItemSourceEnum.DataBase 
        };

        return [song];
    }
}
