using MediatR;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Repositories;

public record UpdateUserCommand(User User) : IRequest<User>;

internal class UpdateUserHanlder: IRequestHandler<UpdateUserCommand, User>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserHanlder(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var updatedUser = await _userRepository.UpsertAsync(request.User, cancellationToken);

        return updatedUser!;
    }
}
