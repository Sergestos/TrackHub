using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackHub.Domain.Entities;

namespace TrackHub.Persistence.Sql.Configurations;

public sealed class UserOrderedPlayedSongConfiguration
    : IEntityTypeConfiguration<UserSongItem>
{
    public void Configure(EntityTypeBuilder<UserSongItem> builder)
    {
        builder.ToTable("user_songs");

        builder.HasKey(x => new { x.UserId, x.DurationPosition });

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.SongName)
            .HasColumnName("song_name")
            .HasMaxLength(512)
            .IsRequired();

        builder.Property(x => x.DurationPosition)
            .HasColumnName("position")
            .IsRequired();

        builder.HasIndex(x => x.UserId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}