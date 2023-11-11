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
    private readonly ITemplateService _templateService;
    private readonly ILogger<TemplateController> _logger;

    public TemplateController(ITemplateService templateService, ILogger<TemplateController> logger)
    {
        _templateService = templateService;
        _logger = logger;
    }
    
    [HttpGet]
    [SwaggerOperation($"Get all {nameof(TemplateDto)}s")]
    [SwaggerResponse(200, type: typeof(IEnumerable<TemplateDto>), description: $"List of {nameof(TemplateDto)}s")]
    [SwaggerResponse(500, type: typeof(ProblemDetails), description: "Server side error")]
    public async Task<ActionResult<IEnumerable<TemplateDto>>> GetAllAsync(CancellationToken ct = default)
    {
        _logger.LogDebug("Get all {name of}s", nameof(TemplateDto));
        var templateDtos = await _templateService.GetAllAsync(ct);
        _logger.LogDebug("Successfully received list of {templateDto}s", nameof(TemplateDto));
        return Ok(templateDtos);
    }

    [HttpGet]
    [SwaggerOperation($"Get all {nameof(TemplateDto)}s")]
    [SwaggerResponse(200, type: typeof(IEnumerable<TemplateDto>), description: $"List of {nameof(TemplateDto)}s")]
    [SwaggerResponse(400, type:typeof(ValidationProblemDetails), description: "Validation error")]
    [SwaggerResponse(500, type: typeof(ProblemDetails), description: "Server side error")]
    public async Task<ActionResult<IEnumerable<TemplateDto>>> GetTemplatesByEnumType(TemplateEnumDto templateEnum, CancellationToken ct = default)
    {
        _logger.LogDebug("Get all {name of}s with state: '{state}'", nameof(TemplateDto), templateEnum);
        var templateDtos = await _templateService.GetTemplatesByEnumTypeAsync(templateEnum, ct);
        _logger.LogDebug("Successfully received list of {templateDto}s with state: '{state}'", nameof(TemplateDto), templateEnum);
        return Ok(templateDtos);
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation($"Get one {nameof(TemplateDto)}")]
    [SwaggerResponse(200, type: typeof(TemplateDto), description: $"Receive one {nameof(TemplateDto)} by id")]
    [SwaggerResponse(400, type:typeof(ValidationProblemDetails), description: "Validation error")]
    [SwaggerResponse(404, type:typeof(ProblemDetails), description: $"{nameof(TemplateDto)} with provided id doesn't exists")]
    [SwaggerResponse(500, type: typeof(ProblemDetails), description: "Server side error")]
    public async Task<ActionResult<TemplateDto>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        _logger.LogDebug("Get {name of} with id: '{id}'", nameof(TemplateDto), id);
        var templateDto = await _templateService.GetByIdAsync(id, ct);
        _logger.LogDebug("Successfully received one {templateDto} by id: '{id}'", nameof(TemplateDto), id);
        return Ok(templateDto);
    }

    [HttpPut("{id:Guid}")]
    [SwaggerOperation($"Update {nameof(TemplateDto)}")]
    [SwaggerResponse(200, type: typeof(TemplateDto), description: $"{nameof(TemplateDto)} successfully updated")]
    [SwaggerResponse(400, type: typeof(ValidationProblemDetails), description: "Validation error")]
    [SwaggerResponse(404, type:typeof(ProblemDetails), description: $"{nameof(TemplateDto)} with provided id doesn't exists")]
    [SwaggerResponse(500, type: typeof(ProblemDetails), description: "Server side error")]
    public async Task<ActionResult<TemplateDto>> UpdateByIdAsync(Guid id, UpdateTemplateDto dtoToUpdate, CancellationToken ct = default)
    {
        _logger.LogDebug("Update {name of} with id: '{id}' and title: '{title}'", nameof(TemplateDto), id, dtoToUpdate.Title);
        var updatedTemplate = await _templateService.UpdateByIdAsync(id, dtoToUpdate, ct);
        _logger.LogDebug("Successfully update {templateDto} by id: '{id}'", nameof(TemplateDto), id);
        
        return Ok(updatedTemplate);
    }

    [HttpDelete("{id:Guid}")]
    [SwaggerOperation($"Delete {nameof(TemplateDto)}")]
    [SwaggerResponse(200, type: typeof(TemplateDto), description: $"{nameof(TemplateDto)} successfully deleted")]
    [SwaggerResponse(400, type: typeof(ValidationProblemDetails), description: "Validation error")]
    [SwaggerResponse(404, type:typeof(ProblemDetails), description: $"{nameof(TemplateDto)} with provided id doesn't exists")]
    [SwaggerResponse(500, type: typeof(ProblemDetails), description: "Server side error")]
    public async Task<ActionResult<Guid>> DeleteByIdAsync(Guid id, CancellationToken ct = default)
    {
        _logger.LogDebug("Delete {name of} with id: '{id}'", nameof(TemplateDto), id);
        var deletedTemplateId = await _templateService.DeleteByIdAsync(id, ct);
        _logger.LogDebug("Successfully delete {templateDto} by id: '{id}'", nameof(TemplateDto), id);
        
        return Ok(deletedTemplateId);
    }
}