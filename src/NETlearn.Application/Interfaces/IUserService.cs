namespace NETlearn.Application.Interfaces;
using NETlearn.Application.DTOs;

public interface IUserService
{
    Task<UserResponse> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default);
    Task<UserResponse?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
}