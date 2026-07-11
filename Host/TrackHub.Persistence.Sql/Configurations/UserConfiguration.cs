using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackHub.Domain.Entities;

namespace TrackHub.Infrastructure.Sql.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.UserId);

        builder.Property(x => x.UserId)
            .HasColumnName("id")
            .HasMaxLength(128);

        builder.Property(x => x.EntityType)
            .HasColumnName("type")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.FullName)
            .HasColumnName("full_name")
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnName("email")
            .HasMaxLength(320)
            .IsRequired();

        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.PhotoUrl)
            .HasColumnName("photo_url")
            .HasMaxLength(2048)
            .IsRequired();

        builder.Property(x => x.RegistrationDate)
            .HasColumnName("registration_date")
            .IsRequired();

        builder.Property(x => x.LastEntranceDate)
            .HasColumnName("last_entrance_date");

        builder.Property(x => x.LastPlayDate)
            .HasColumnName("last_play_date");

        builder.Property(x => x.FirstPlayDate)
            .HasColumnName("first_play_date");

        builder.HasOne(x => x.LoginSession)
            .WithOne()
            .HasForeignKey<LoginSession>("user_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}