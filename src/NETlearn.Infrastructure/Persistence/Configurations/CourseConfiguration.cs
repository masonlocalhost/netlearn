using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NETlearn.Domain.Entities;

namespace NETlearn.Infrastructure.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        // Table name
        builder.ToTable("Courses");

        // Primary Key
        builder.HasKey(u => u.Id);

        // Properties & Constraints
        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.Topic)
            .IsRequired()
            .HasMaxLength(50);


        builder.HasMany(c => c.Users)
            .WithMany(u => u.Courses)
            .UsingEntity<CourseUser>(
                r => r.HasOne(c => c.User)
                    .WithMany(u => u.CourseUsers)
                    .HasForeignKey(cu => cu.UserId),
                l => l.HasOne(c => c.Course)
                    .WithMany(c => c.CourseUsers)
                    .HasForeignKey(cu => cu.CourseId),
                j => j.HasKey(cu => new { cu.UserId, cu.CourseId })
            );
    }
}
