﻿using TrackHub.Domain.Entities;

namespace TrackHub.Domain.Repositories;

public interface IUserRepository
{
    Task<User> GetUserAsync(string userId, CancellationToken cancellationToken);

    Task<User> RegistrateUser(User user, CancellationToken cancellationToken);

    Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken);

    Task DeleteUserAsync(string userId, CancellationToken cancellationToken);
}
