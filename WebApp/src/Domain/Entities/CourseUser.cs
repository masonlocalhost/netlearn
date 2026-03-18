namespace WebApp.Domain.Entities;

public class CourseUser
{
    public int CourseId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public Course Course { get; set; } = null!;
}
