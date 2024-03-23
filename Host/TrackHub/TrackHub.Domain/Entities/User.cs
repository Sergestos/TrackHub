using System.ComponentModel.DataAnnotations.Schema;

namespace TrackHub.Domain.Entities;

[Table("user")]
public class User
{
    [Column("id")]
    public required int Id { get; set; }

    [Column("nickName")]
    public required string NickName { get; set; }

    [Column("email")]
    public required string Email { get; set; }

    [Column("passwordHash")]
    public required string PasswordHash { get; set; }
}
