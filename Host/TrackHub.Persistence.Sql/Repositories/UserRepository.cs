using Microsoft.EntityFrameworkCore;
using TrackHub.Domain.Repositories;
using TrackHub.Infrastructure.Sql.Context;
using DomainUser = TrackHub.Domain.Entities.User;

namespace TrackHub.Persistence.Sql.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly ITrackHubDbContext _context;

    public UserRepository(ITrackHubDbContext context)
    {
        _context = context;
    }

    public DomainUser? GetUserById(string userId)
    {
        return _context.Users
            .Include(x => x.LoginSession)
            .Include(x => x.UserSongItem)
            .FirstOrDefault(x => x.UserId == userId);
    }

    public DomainUser? GetUserByEmail(string userEmail)
    {
        return _context.Users
            .Include(x => x.LoginSession)
            .Include(x => x.UserSongItem)
            .FirstOrDefault(x => x.Email == userEmail);
    }

    public async Task<DomainUser?> UpsertAsync(
        DomainUser user,
        CancellationToken cancellationToken)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(
                x => x.UserId == user.UserId,
                cancellationToken);

        if (existingUser is null)
        {
            _context.Users.Add(user);
        }
        else
        {
            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.PhotoUrl = user.PhotoUrl;
            existingUser.RegistrationDate = user.RegistrationDate;
            existingUser.LastEntranceDate = user.LastEntranceDate;
            existingUser.LastPlayDate = user.LastPlayDate;
            existingUser.FirstPlayDate = user.FirstPlayDate;
            existingUser.LoginSession = user.LoginSession;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return user;
    }
}