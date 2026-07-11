using MediatR;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Repositories;
using TrackHub.Service.UserServices.Models;

public record GetOrUpsertUserCommand(SocialUserModel User) : IRequest<User>;

internal class GetOrUpsertUserHandler: IRequestHandler<GetOrUpsertUserCommand, User>
{
    private readonly IUserRepository _userRepository;

    public GetOrUpsertUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle(
        GetOrUpsertUserCommand request,
        CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetUserByEmailAsync(request.User.Email, cancellationToken);
        if (user == null)
        {
            var newUser = new User()
            {
                UserId = Guid.NewGuid().ToString(),
                Email = request.User.Email,
                FullName = request.User.FullName,
                PhotoUrl = request.User.PhotoUrl,
                RegistrationDate = DateTime.UtcNow,
                LastEntranceDate = DateTime.UtcNow
            };

            user = await _userRepository.UpsertAsync(newUser, cancellationToken);
        }
        else
        {
            user.LastEntranceDate = DateTime.UtcNow;
            await _userRepository.UpsertAsync(user, cancellationToken);
        }

        return user!;
    }
}