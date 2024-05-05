using TrackHub.Domain;
using TrackHub.Domain.Enums;

namespace TrackHub.Service.Exercises.Implementation;

internal class SongSearchService : ISearchable<Song>
{
    public SongSearchService() { }

    public async Task<IEnumerable<Song>> SearchAsync(string searchText, CancellationToken cancellationToken)
    {
        var song = new Song() 
        { 
            Name  = "hello", 
            Source = ItemSourceEnum.DataBase 
        };

        return [song];
    }
}
