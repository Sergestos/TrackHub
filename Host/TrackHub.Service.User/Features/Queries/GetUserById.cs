using MediatR;
using TrackHub.Domain.Repositories;

namespace TrackHub.Service.User.Features.Queries;

public record GetUserByIdQuery(string userId) : IRequest<Domain.Entities.User>;

internal class GetUserByIdHandler: IRequestHandler<GetUserByIdQuery, Domain.Entities.User>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Domain.Entities.User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return (await _userRepository.GetUserByIdAsync(request.userId, cancellationToken))!;
    }
}