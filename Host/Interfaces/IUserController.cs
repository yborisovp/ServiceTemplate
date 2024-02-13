using Microsoft.AspNetCore.Mvc;
using ServiceTemplate.DataContracts.Dtos.Users;
using ServiceTemplate.DataContracts.Filters;

namespace ServiceTemplate.Interfaces;

public interface IUserController: IBaseController<UserDto, long, CreateUserDto, UpdateUserDto>
{
    public Task<ActionResult<FilterResultDto<UserDto>>> GetAllAsync(UserFiltersDto filtersDto, CancellationToken ct = default);
}