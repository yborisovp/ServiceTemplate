using Microsoft.AspNetCore.Mvc;
using ServiceTemplate.DataContracts.Dtos.Users;
using ServiceTemplate.DataContracts.Filters;
using ServiceTemplate.DataContracts.Interfaces;
using ServiceTemplate.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceTemplate.Controllers;

/// <summary>
/// API to control users
/// </summary>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class UserController: IUserController
{
    private readonly IUserService _service;
    private readonly ILogger<UserController> _logger;
    
    public UserController(IUserService service, ILogger<UserController> logger)
    {
        _service = service;
        _logger = logger;
    }
    
    /// <summary>
    /// Get all users
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns>List of users</returns>
    [HttpGet]
    [SwaggerOperation($"Get all {nameof(UserDto)}s")]
    [SwaggerResponse(200, type: typeof(IEnumerable<UserDto>), description: $"List of {nameof(UserDto)}s")]
    [SwaggerResponse(500, type: typeof(ProblemDetails), description: "Server side error")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllAsync(CancellationToken ct = default)
    {
        _logger.LogDebug("Get all {name of}s", nameof(UserDto));
        var result = await _service.GetAllAsync(ct);
        _logger.LogDebug("Successfully received list of {result}s", nameof(UserDto));
        return new ObjectResult(result);
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <param name="filtersDto">Filters for users</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>List of users</returns>
    [HttpPost("filter")]
    [SwaggerOperation($"Get all {nameof(UserDto)}s")]
    [SwaggerResponse(200, type: typeof(IEnumerable<UserDto>), description: $"List of {nameof(UserDto)}s")]
    [SwaggerResponse(500, type: typeof(ProblemDetails), description: "Server side error")]
    
    public async Task<ActionResult<FilterResultDto<UserDto>>> GetAllAsync([FromBody] UserFiltersDto filtersDto, CancellationToken ct = default)
    {
        _logger.LogDebug("Get all {name of}s by filter", nameof(UserDto));
        var result = await _service.GetAllAsync(filtersDto, ct);
        _logger.LogDebug("Successfully received filtered list of {result}s", nameof(UserDto));
        return new ObjectResult(result);
    }
    
    /// <summary>
    /// Get user by Id
    /// </summary>
    /// <param name="id">Id of user</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Recipe with provided Id</returns>
    [HttpGet("{id:long}")]
    [SwaggerOperation($"Get one {nameof(UserDto)}")]
    [SwaggerResponse(200, type: typeof(UserDto), description: $"Receive one {nameof(UserDto)} by id")]
    [SwaggerResponse(400, type:typeof(ValidationProblemDetails), description: "Validation error")]
    [SwaggerResponse(404, type:typeof(ProblemDetails), description: $"{nameof(UserDto)} with provided id doesn't exists")]
    [SwaggerResponse(500, type: typeof(ProblemDetails), description: "Server side error")]
    public async Task<ActionResult<UserDto>> GetByIdAsync(long id, CancellationToken ct = default)
    {
        _logger.LogDebug("Get {name of} with id: '{id}'", nameof(UserDto), id);
        var result = await _service.GetByIdAsync(id, ct);
        _logger.LogDebug("Successfully received one {result} by id: '{id}'", nameof(UserDto), id);
        return new ObjectResult(result);
    }

    /// <summary>
    /// Create new user
    /// </summary>
    /// <param name="dtoToCreate">New entity</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>List of users</returns>
    [HttpPost]
    [SwaggerOperation($"Create {nameof(UserDto)}")]
    [SwaggerResponse(200, type: typeof(IEnumerable<UserDto>), description: $"Created {nameof(UserDto)}")]
    [SwaggerResponse(500, type: typeof(ProblemDetails), description: "Server side error")]
    public async Task<ActionResult<UserDto>> CreateAsync(CreateUserDto dtoToCreate, CancellationToken ct = default)
    {
        _logger.LogDebug("Create {name of} with UserName: '{UserName}'", nameof(UserDto), dtoToCreate.UserName);
        var result = await _service.CreateAsync(dtoToCreate, ct);
        _logger.LogDebug("Successfully create {result} with id: '{id}'", nameof(UserDto), result.Id);
        return new ObjectResult(result);
    }

    /// <summary>
    /// Update user by Id
    /// </summary>
    /// <param name="id">Id of user</param>
    /// <param name="dtoToUpdate">DTO Recipe with updated fields</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Updated user</returns>
    [HttpPut("{id:long}")]
    [SwaggerOperation($"Update {nameof(UserDto)}")]
    [SwaggerResponse(200, type: typeof(UserDto), description: $"{nameof(UserDto)} successfully updated")]
    [SwaggerResponse(400, type: typeof(ValidationProblemDetails), description: "Validation error")]
    [SwaggerResponse(404, type:typeof(ProblemDetails), description: $"{nameof(UserDto)} with provided id doesn't exists")]
    [SwaggerResponse(500, type: typeof(ProblemDetails), description: "Server side error")]
    public async Task<ActionResult<UserDto>> UpdateByIdAsync(long id, UpdateUserDto dtoToUpdate, CancellationToken ct = default)
    {
        _logger.LogDebug("Update {name of} with id: '{id}' and UserName: '{UserName}'", nameof(UserDto), id, dtoToUpdate.UserName);
        var result = await _service.UpdateByIdAsync(id, dtoToUpdate, ct);
        _logger.LogDebug("Successfully update {result} by id: '{id}'", nameof(UserDto), id);
        
        return new ObjectResult(result);
    }

    /// <summary>
    /// Delete user
    /// </summary>
    /// <param name="id">Id of user</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Id of deleted user</returns>
    [HttpDelete("{id:long}")]
    [SwaggerOperation($"Delete {nameof(UserDto)}")]
    [SwaggerResponse(200, type: typeof(UserDto), description: $"{nameof(UserDto)} successfully deleted")]
    [SwaggerResponse(400, type: typeof(ValidationProblemDetails), description: "Validation error")]
    [SwaggerResponse(404, type:typeof(ProblemDetails), description: $"{nameof(UserDto)} with provided id doesn't exists")]
    [SwaggerResponse(500, type: typeof(ProblemDetails), description: "Server side error")]
    public async Task<ActionResult<long>> DeleteByIdAsync(long id, CancellationToken ct = default)
    {
        _logger.LogDebug("Delete {name of} with id: '{id}'", nameof(UserDto), id);
        var result = await _service.DeleteByIdAsync(id, ct);
        _logger.LogDebug("Successfully delete {result} by id: '{id}'", nameof(UserDto), id);
        
        return new ObjectResult(result);
    }

}