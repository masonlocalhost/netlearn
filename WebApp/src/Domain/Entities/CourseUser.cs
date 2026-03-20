namespace WebApp.Domain.Entities;

public class CourseUser
{
    public Guid CourseId { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Course Course { get; set; } = null!;
}
