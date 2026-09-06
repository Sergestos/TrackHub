using MediatR;
using TrackHub.Domain.Repositories;
using TrackHub.Service.User.Models;

namespace TrackHub.Service.User.Features.Queries;

public record GetUserSettingsQuery(string userId) : IRequest<UserSettingsModel>;

internal class GetUserSettingsHandler: IRequestHandler<GetUserSettingsQuery, UserSettingsModel>
{
    private readonly IUserRepository _userRepository;

    public GetUserSettingsHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserSettingsModel> Handle(GetUserSettingsQuery request, CancellationToken cancellationToken)
    {
        DateTimeOffset offset = default;

        var user = await _userRepository.GetUserByIdAsync(request.userId, cancellationToken);
        if (user!.FirstPlayDate.HasValue)
            offset = user.FirstPlayDate!.Value;
        else
            offset = DateTimeOffset.UtcNow;

        return new UserSettingsModel(offset);
    }
}
