namespace NETlearn.Domain.Entities;

public class SafeKey
{
    public Guid Id { get; set; }
    public string Secret { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
