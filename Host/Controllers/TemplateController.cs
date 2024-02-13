using Microsoft.AspNetCore.Mvc;
using ServiceTemplate.DataContracts.Dtos.Templates;
using ServiceTemplate.DataContracts.Dtos.Templates.Enums;
using ServiceTemplate.DataContracts.Interfaces;
using ServiceTemplate.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceTemplate.Controllers;

/// <summary>
/// API to control templates
/// </summary>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class TemplateController: ControllerBase, ITemplateController
{
    private readonly ITemplateService _service;
    private readonly ILogger<TemplateController> _logger;

    /// <summary>
    /// Constructor of TemplateController
    /// </summary>
    /// <param name="service"></param>
    /// <param name="logger"></param>
    public TemplateController(ITemplateService service, ILogger<TemplateController> logger)
    {
        _service = service;
        _logger = logger;
    }
    
    /// <summary>
    /// Get all templates
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns>List of templates</returns>
    [HttpGet]
    [SwaggerOperation($"Get all {nameof(TemplateDto)}s")]
    [SwaggerResponse(200, type: typeof(IEnumerable<TemplateDto>), description: $"List of {nameof(TemplateDto)}s")]
    [SwaggerResponse(500, type: typeof(ProblemDetails), description: "Server side error")]
    public async Task<ActionResult<IEnumerable<TemplateDto>>> GetAllAsync(CancellationToken ct = default)
    {
        _logger.LogDebug("Get all {name of}s", nameof(TemplateDto));
        var templateDtos = await _service.GetAllAsync(ct);
        _logger.LogDebug("Successfully received list of {templateDto}s", nameof(TemplateDto));
        return Ok(templateDtos);
    }

    /// <summary>
    /// Get template by enum type
    /// </summary>
    /// <param name="templateEnum">Enum type</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>List of templates with provided enum type</returns>
    [HttpGet("/{templateEnum}")]
    [SwaggerOperation($"Get {nameof(TemplateDto)}s by enum")]
    [SwaggerResponse(200, type: typeof(IEnumerable<TemplateDto>), description: $"List of {nameof(TemplateDto)}s")]
    [SwaggerResponse(400, type:typeof(ValidationProblemDetails), description: "Validation error")]
    [SwaggerResponse(500, type: typeof(ProblemDetails), description: "Server side error")]
    public async Task<ActionResult<IEnumerable<TemplateDto>>> GetTemplatesByEnumType(TemplateEnumDto templateEnum, CancellationToken ct = default)
    {
        _logger.LogDebug("Get all {name of}s with state: '{state}'", nameof(TemplateDto), templateEnum);
        var templateDtos = await _service.GetTemplatesByEnumTypeAsync(templateEnum, ct);
        _logger.LogDebug("Successfully received list of {templateDto}s with state: '{state}'", nameof(TemplateDto), templateEnum);
        return Ok(templateDtos);
    }

    /// <summary>
    /// Get template by Id
    /// </summary>
    /// <param name="id">Id of template</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Template with provided Id</returns>
    [HttpGet("{id:guid}")]
    [SwaggerOperation($"Get one {nameof(TemplateDto)}")]
    [SwaggerResponse(200, type: typeof(TemplateDto), description: $"Receive one {nameof(TemplateDto)} by id")]
    [SwaggerResponse(400, type:typeof(ValidationProblemDetails), description: "Validation error")]
    [SwaggerResponse(404, type:typeof(ProblemDetails), description: $"{nameof(TemplateDto)} with provided id doesn't exists")]
    [SwaggerResponse(500, type: typeof(ProblemDetails), description: "Server side error")]
    public async Task<ActionResult<TemplateDto>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        _logger.LogDebug("Get {name of} with id: '{id}'", nameof(TemplateDto), id);
        var templateDto = await _service.GetByIdAsync(id, ct);
        _logger.LogDebug("Successfully received one {templateDto} by id: '{id}'", nameof(TemplateDto), id);
        return Ok(templateDto);
    }
    
    /// <summary>
    /// Create new user
    /// </summary>
    /// <param name="dtoToCreate">New entity</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>List of users</returns>
    [HttpPost]
    [SwaggerOperation($"Create {nameof(TemplateDto)}")]
    [SwaggerResponse(200, type: typeof(IEnumerable<TemplateDto>), description: $"Created {nameof(TemplateDto)}")]
    [SwaggerResponse(500, type: typeof(ProblemDetails), description: "Server side error")]
    public async Task<ActionResult<TemplateDto>> CreateAsync(CreateTemplateDto dtoToCreate, CancellationToken ct = default)
    {
        _logger.LogDebug("Create {name of} with Title: '{Title}'", nameof(TemplateDto), dtoToCreate.Title);
        var result = await _service.CreateAsync(dtoToCreate, ct);
        _logger.LogDebug("Successfully create {result} with id: '{id}'", nameof(TemplateDto), result.Id);
        return new ObjectResult(result);
    }

    /// <summary>
    /// Update template by Id
    /// </summary>
    /// <param name="id">Id of template</param>
    /// <param name="dtoToUpdate">DTO Template with updated fields</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Updated template</returns>
    [HttpPut("{id:Guid}")]
    [SwaggerOperation($"Update {nameof(TemplateDto)}")]
    [SwaggerResponse(200, type: typeof(TemplateDto), description: $"{nameof(TemplateDto)} successfully updated")]
    [SwaggerResponse(400, type: typeof(ValidationProblemDetails), description: "Validation error")]
    [SwaggerResponse(404, type:typeof(ProblemDetails), description: $"{nameof(TemplateDto)} with provided id doesn't exists")]
    [SwaggerResponse(500, type: typeof(ProblemDetails), description: "Server side error")]
    public async Task<ActionResult<TemplateDto>> UpdateByIdAsync(Guid id, [FromBody] UpdateTemplateDto dtoToUpdate, CancellationToken ct = default)
    {
        _logger.LogDebug("Update {name of} with id: '{id}' and title: '{title}'", nameof(TemplateDto), id, dtoToUpdate.Title);
        var updatedTemplate = await _service.UpdateByIdAsync(id, dtoToUpdate, ct);
        _logger.LogDebug("Successfully update {templateDto} by id: '{id}'", nameof(TemplateDto), id);
        
        return Ok(updatedTemplate);
    }

    /// <summary>
    /// Delete template
    /// </summary>
    /// <param name="id">Id of template</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Id of deleted template</returns>
    [HttpDelete("{id:Guid}")]
    [SwaggerOperation($"Delete {nameof(TemplateDto)}")]
    [SwaggerResponse(200, type: typeof(TemplateDto), description: $"{nameof(TemplateDto)} successfully deleted")]
    [SwaggerResponse(400, type: typeof(ValidationProblemDetails), description: "Validation error")]
    [SwaggerResponse(404, type:typeof(ProblemDetails), description: $"{nameof(TemplateDto)} with provided id doesn't exists")]
    [SwaggerResponse(500, type: typeof(ProblemDetails), description: "Server side error")]
    public async Task<ActionResult<Guid>> DeleteByIdAsync(Guid id, CancellationToken ct = default)
    {
        _logger.LogDebug("Delete {name of} with id: '{id}'", nameof(TemplateDto), id);
        var deletedTemplateId = await _service.DeleteByIdAsync(id, ct);
        _logger.LogDebug("Successfully delete {templateDto} by id: '{id}'", nameof(TemplateDto), id);
        
        return Ok(deletedTemplateId);
    }
}