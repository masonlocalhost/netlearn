namespace WebApp.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public ICollection<SafeKey> SafeKeys { get; set; } = [];
    // public ICollection<CourseUser> CourseUsers { get; } = [];
    // public ICollection<Course> Courses { get; set; } = [];
}
