using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core configuration for the RefreshToken entity.
/// </summary>
public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Token)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(t => t.ExpiresAt)
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .IsRequired();

        builder.Property(t => t.RevokedAt);

        builder.Property(t => t.UserId)
            .IsRequired();

        // Index on token for fast lookups
        builder.HasIndex(t => t.Token);

        // Index on UserId for cascading operations
        builder.HasIndex(t => t.UserId);

        // Composite index for active token lookups
        builder.HasIndex(t => new { t.Token, t.RevokedAt, t.ExpiresAt });
    }
}
