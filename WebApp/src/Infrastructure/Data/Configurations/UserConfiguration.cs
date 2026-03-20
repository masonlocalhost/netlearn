using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Domain.Entities;

namespace WebApp.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Table name
        builder.ToTable("Users");

        // Primary Key
        builder.HasKey(u => u.Id);

        // Properties & Constraints
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(150);

        // Indexes
        builder.HasIndex(u => u.Email)
            .IsUnique(); // Ensure no duplicate emails at the DB level

        builder.HasMany(u => u.SafeKeys)
            .WithOne(k => k.User)
            .HasForeignKey(k => k.UserId)
            .IsRequired();

        builder.HasMany(u => u.Courses)
            .WithMany(c => c.Users)
            .UsingEntity<CourseUser>(
                l => l.HasOne(cu => cu.Course)
                    .WithMany(c => c.CourseUsers)
                    .HasForeignKey(cu => cu.CourseId),
                r => r.HasOne(cu => cu.User)
                    .WithMany(u => u.CourseUsers)
                    .HasForeignKey(cu => cu.UserId),

                j => j.HasKey(cu => new { cu.UserId, cu.CourseId })
            );
    }
}
