using System.ComponentModel.DataAnnotations.Schema;

namespace TrackHub.Service.Users.Models;

[Table("user")]
public record UserViewModel
{
    [Column("id")]
    public required string Id { get; set; }

    [Column("nickName")]
    public required string NickName { get; set; }

    [Column("email")]
    public required string Email{ get; set; }

    [Column("passwordHash")]
    public required string PasswordHash { get; set; }

    [Column("registrationDate")]
    public DateTime? RegistrationDate { get; set; }
}
