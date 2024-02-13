using ServiceTemplate.DataContracts.Dtos.Users;
using ServiceTemplate.DataContracts.Filters;

namespace ServiceTemplate.DataContracts.Interfaces;

public interface IUserService: IBaseService<UserDto, long, CreateUserDto, UpdateUserDto>
{
    public Task<FilterResultDto<UserDto>> GetAllAsync(UserFiltersDto filtersDto, CancellationToken ct = default);
    public Task<bool> IsUserExist(long userId, CancellationToken ct = default);
}