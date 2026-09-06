using Microsoft.EntityFrameworkCore;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Repositories;
using TrackHub.Persistence.Sql.Context;
using DomainUser = TrackHub.Domain.Entities.User;

namespace TrackHub.Persistence.Sql.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly ITrackHubDbContext _context;

    public UserRepository(ITrackHubDbContext context)
    {
        _context = context;
    }

    public async Task<DomainUser?> GetUserByIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(x => x.LoginSession)
            .Include(x => x.UserSongItems)
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }

    public async Task<DomainUser?> GetUserByEmailAsync(string userEmail, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(x => x.LoginSession)
            .Include(x => x.UserSongItems)
            .FirstOrDefaultAsync(x => x.Email == userEmail, cancellationToken);
    }

    public async Task<DomainUser?> UpsertAsync(
        DomainUser user,
        CancellationToken cancellationToken)
    {
        var existingUser = await _context.Users
            .Include(x => x.LoginSession)
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

            if (user.LoginSession is not null)
            {
                if (existingUser.LoginSession is null)
                {
                    existingUser.LoginSession = user.LoginSession;
                }
                else
                {                
                    existingUser.LoginSession.SessionId = user.LoginSession.SessionId;
                    existingUser.LoginSession.CreatedAt = user.LoginSession.CreatedAt;
                    existingUser.LoginSession.ExpiresAt = user.LoginSession.ExpiresAt;
                }
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return existingUser ?? user;
    }
}