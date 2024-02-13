using ServiceTemplate.DataAccess.Interfaces;
using ServiceTemplate.DataContracts.Dtos.Users;
using ServiceTemplate.DataContracts.Filters;
using ServiceTemplate.DataContracts.Interfaces;
using ServiceTemplate.Mappers;
using ServiceTemplate.Mappers.Filters;

namespace ServiceTemplate.Services;

/// <summary>
/// User service
/// </summary>
public class UserService: IUserService
{
    private readonly IUserRepository _repository;
    private readonly ILogger<UserService> _logger;

    /// <summary>
    /// Constructor of Users service
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="logger"></param>
    public UserService(IUserRepository repository, ILogger<UserService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    ///<inheritdoc/>
    public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken ct = default)
    {
        _logger.LogDebug("Getting all users");
        var result = await _repository.GetAllAsync(ct);
        return result.Select(UserMapper.ToDto);
    }
    
    ///<inheritdoc/>
    public async Task<FilterResultDto<UserDto>> GetAllAsync(UserFiltersDto filtersDto, CancellationToken ct = default)
    {
        _logger.LogDebug("Get filtered users by filter {@id}", filtersDto);
        var result = await _repository.GetAllAsync(filtersDto.ToEntity(), ct);
        return result.ToDto();
    }

    ///<inheritdoc/>
    public async Task<bool> IsUserExist(long userId, CancellationToken ct = default)
    {
        return await _repository.IsUserExist(userId, ct);
    }

    ///<inheritdoc/>
    public async Task<UserDto> GetByIdAsync(long id, CancellationToken ct = default)
    {
        _logger.LogDebug("Getting user by id {id}", id);
        var result = await _repository.GetByIdAsync(id, ct);
        return result.ToDto();
    }

    ///<inheritdoc/>
    public async Task<UserDto> CreateAsync(CreateUserDto dtoToCreate, CancellationToken ct = default)
    {
        _logger.LogDebug("Create new user with UserName {userName}", dtoToCreate.UserName);
        if (string.IsNullOrEmpty(dtoToCreate.UserName))
        {
            dtoToCreate.UserName = Guid.NewGuid().ToString();
        }
        var entity = await _repository.CreateAsync(dtoToCreate.ToEntity(), ct);
        return entity.ToDto();
    }

    
    ///<inheritdoc/>
    public async Task<UserDto> UpdateByIdAsync(long id, UpdateUserDto dtoToUpdate, CancellationToken ct = default)
    {
        _logger.LogDebug("Updating user by id {id}", id);
        var entity = await _repository.GetByIdAsync(id, ct);
        var entityToUpdate = dtoToUpdate.ToEntity(entity);
        await _repository.UpdateAsync(entityToUpdate, ct);
        return entity.ToDto();
    }

    ///<inheritdoc/>
    public async Task<long> DeleteByIdAsync(long id, CancellationToken ct = default)
    {
        _logger.LogDebug("Deleting user by id {id}", id);
        var entity = await _repository.GetByIdAsync(id, ct);
        await _repository.DeleteByIdAsync(id, ct);
        return entity.Id;
    }

}