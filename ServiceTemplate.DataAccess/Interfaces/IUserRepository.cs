using ServiceTemplate.DataAccess.Filtering;
using ServiceTemplate.DataAccess.Models.Users;

namespace ServiceTemplate.DataAccess.Interfaces;

public interface IUserRepository : IRepository<User, long>
{
    public Task<FilterResult<User>> GetAllAsync(UserFilters filters, CancellationToken ct = default);
    public Task<bool> IsUserExist(long userId, CancellationToken ct);
}