
namespace TrackHub.Service.Users.Models;

public record UserCreateModel
{
    public required string NickName { get; set; }

    public required string Email { get; set; }

    public required string PasswordHash { get; set; }
}
