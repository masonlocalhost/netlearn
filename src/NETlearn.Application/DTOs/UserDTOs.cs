namespace NETlearn.Application.DTOs;

// What the client sends us
public record CreateUserRequest(string Email, string FullName);

// What we send back to the client (Notice we don't send back the internal DB ID if we don't want to, or we format things differently)
public record UserResponse(Guid Id, string Email, string FullName, DateTime CreatedAt);

public record CreateUserResponse(UserResponse User);

public record GetUserResponse(UserResponse User);