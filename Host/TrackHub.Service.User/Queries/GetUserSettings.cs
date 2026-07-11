using MediatR;
using TrackHub.Domain.Repositories;

namespace TrackHub.Application.Service.User.Queries;

public record UserSettings(DateTimeOffset FirstPlayDate);

public record GetUserSettingsQuery(string userId) : IRequest<UserSettings>;

public class GetUserSettingsHandler: IRequestHandler<GetUserSettingsQuery, UserSettings>
{
    private readonly IUserRepository _userRepository;

    public GetUserSettingsHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserSettings> Handle(GetUserSettingsQuery request, CancellationToken cancellationToken)
    {
        DateTimeOffset offset = default;

        var user = await _userRepository.GetUserByIdAsync(request.userId, cancellationToken);
        if (user!.FirstPlayDate.HasValue)
            offset = user.FirstPlayDate!.Value;
        else
            offset = DateTimeOffset.UtcNow;

        return new UserSettings(offset);
    }
}
