// Location: src/Nitro.Application/Services/UserService.cs
using NETlearn.Application.DTOs;
using NETlearn.Application.Interfaces;
using NETlearn.Domain.Entities;

namespace NETlearn.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<UserResponse> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        // 1. Business Logic: Check for duplicates
        if (await userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
        {
            // In a real app, you might throw a custom exception here (e.g., ConflictException) 
            // that a global middleware catches and turns into a 409 Conflict HTTP response.
            throw new InvalidOperationException("Email already exists.");
        }

        // 2. Map DTO to Domain Entity
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            FullName = request.FullName,
            CreatedAt = DateTime.UtcNow
        };

        // 3. Save to database
        await userRepository.AddAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);

        // 4. Map Entity back to Response DTO
        return new UserResponse(user.Id, user.Email, user.FullName, user.CreatedAt);
    }

    public async Task<UserResponse?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(id, cancellationToken);
        
        if (user is null) return null;

        return new UserResponse(user.Id, user.Email, user.FullName, user.CreatedAt);
    }
}