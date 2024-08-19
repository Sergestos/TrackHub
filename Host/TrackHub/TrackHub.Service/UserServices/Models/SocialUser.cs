namespace TrackHub.Service.UserServices.Models;

public class SocialUser
{
    public required string UserId { get; set; }

    public required string Email { get; set; }

    public required string FullName { get; set; }

    public required string PhotoUrl { get; set; }
}
