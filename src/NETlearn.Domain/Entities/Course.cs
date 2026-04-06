namespace NETlearn.Domain.Entities;

public class Course
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<CourseUser> CourseUsers { get; } = [];
    public ICollection<User> Users { get; set; } = [];
}
