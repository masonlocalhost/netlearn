using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NETlearn.Domain.Entities;

namespace NETlearn.Infrastructure.Persistence.Configurations;

public class SafeKeyConfiguration : IEntityTypeConfiguration<SafeKey>
{
    public void Configure(EntityTypeBuilder<SafeKey> builder)
    {
        // Table name
        builder.ToTable("SafeKeys");

        // Primary Key
        builder.HasKey(u => u.Id);

        // Properties & Constraints
        builder.Property(u => u.Secret)
            .IsRequired()
            .HasMaxLength(255);

        // Relation
        builder.HasOne(k => k.User)
            .WithMany(user => user.SafeKeys)
            .HasForeignKey(k => k.UserId)
            .IsRequired();
    }
}
