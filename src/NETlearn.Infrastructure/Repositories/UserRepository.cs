using Microsoft.EntityFrameworkCore;
using NETlearn.Application.Interfaces;
using NETlearn.Domain.Entities;
using NETlearn.Infrastructure.Persistence;

namespace NETlearn.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context): IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        // AsNoTracking() is a massive performance boost for read-only queries.
        // If we just need to check the data but not update it immediately, we use this.
        return await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        // AnyAsync is much faster than pulling the whole record to check existence
        return await context.Users
            .AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await context.Users.AddAsync(user, cancellationToken);
    }

    public void Update(User user)
    {
        // EF Core tracks updates automatically if the entity was queried without AsNoTracking.
        // Explicitly calling Update is good practice if the entity is detached.
        context.Users.Update(user);
    }

    public void Delete(User user)
    {
        context.Users.Remove(user);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}