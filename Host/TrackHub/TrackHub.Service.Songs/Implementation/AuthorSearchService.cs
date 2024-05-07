﻿using TrackHub.Domain.Entities;
using TrackHub.Domain.Enums;

namespace TrackHub.Service.Exercises.Implementation;

internal class AuthorSearchService : IAuthorService
{
    public AuthorSearchService() { }

    public async Task<IEnumerable<Author>> SearchAsync(string searchText, CancellationToken cancellationToken)
    {
        var author = new Author()
        {
            Name = "hello",
            Source = ItemSourceEnum.DataBase
        };

        return [author];
    }
}
