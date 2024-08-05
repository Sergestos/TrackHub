namespace TrackHub.Web.Models;

public class SocialUser
{
    public required string IdToken { get; set; }

    public required string Email { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string PhotoUrl { get; set; }
}
