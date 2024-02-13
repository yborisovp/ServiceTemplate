using FR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using ServiceTemplate.DataAccess.Context;
using ServiceTemplate.DataAccess.Filtering;
using ServiceTemplate.DataAccess.Interfaces;
using ServiceTemplate.DataAccess.Models.Users;

namespace ServiceTemplate.DataAccess.Repositories;

public class UserRepository(
    IDatabaseContextFactory<DatabaseContext> contextFactory,
    IDatabaseContextFactory<IdentityDatabaseContext> identityDatabaseContext)
    : BaseRepository(contextFactory, identityDatabaseContext), IUserRepository
{
    
    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken ct = default)
    {
        await using var context = IdentityContextFactory.CreateDbContext();
        return await context.Users.ToListAsync(ct);
    }
    
    public async Task<FilterResult<User>> GetAllAsync(UserFilters filters, CancellationToken ct = default)
    {
        await using var context = IdentityContextFactory.CreateDbContext();
        var query = context.Users.AsQueryable();

        if (filters.UserName is not null)
        {
            query = query.Where(u => u.UserName != null && u.UserName.Contains(filters.UserName));
        }
        
        var skipSize = (filters.PageNumber - 1) * filters.PageSize;

        var count = query.Count();
        var result = await query.Skip(skipSize).Take(filters.PageSize).ToListAsync(ct);
        return new FilterResult<User>
        {
            Results = result,
            Count = count
        };
    }

    public async Task<bool> IsUserExist(long userId, CancellationToken ct)
    {
        await using var context = IdentityContextFactory.CreateDbContext();
        await GetByIdAsync(userId, ct);
        return true;
    }

    public async Task<User> GetByIdAsync(long id, CancellationToken ct = default)
    {
        await using var context = IdentityContextFactory.CreateDbContext();
        
        try
        {
            return await context.Users.SingleAsync(u => u.Id == id, cancellationToken: ct);
        }
        catch (InvalidOperationException)
        {
            throw new Exception();
        }
    }

    public async Task<User> CreateAsync(User entityToCreate, CancellationToken ct = default)
    {
        await using var context = IdentityContextFactory.CreateDbContext();
        var entity = await context.Users.AddAsync(entityToCreate, ct);
        await context.SaveChangesAsync(ct);
        var result = await GetByIdAsync(entity.Entity.Id, ct);
        return result;
    }

    public async Task<User> UpdateAsync(User entityToUpdate, CancellationToken ct = default)
    {
        await using var context = IdentityContextFactory.CreateDbContext();
        context.Users.Update(entityToUpdate);
        await context.SaveChangesAsync(ct);
        var result = await GetByIdAsync(entityToUpdate.Id, ct);
        return result;
    }

    public async Task<long> DeleteByIdAsync(long id, CancellationToken ct = default)
    {
        await using var context = IdentityContextFactory.CreateDbContext();
        var recipe = await context.Users.SingleAsync(u => u.Id == id, cancellationToken: ct);
       
        context.Users.Remove(recipe);
        await context.SaveChangesAsync(ct);
        return id;
    }
}