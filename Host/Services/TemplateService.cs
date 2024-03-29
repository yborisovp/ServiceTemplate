using ServiceTemplate.DataAccess.Interfaces;
using ServiceTemplate.DataAccess.Models.Templates;
using ServiceTemplate.DataContracts.Dtos.Templates;
using ServiceTemplate.DataContracts.Dtos.Templates.Enums;
using ServiceTemplate.DataContracts.Interfaces;
using ServiceTemplate.Mappers.Templates;

namespace ServiceTemplate.Services;

/// <summary>
/// Service to grant access to db entities
/// </summary>
public class TemplateService: ITemplateService
{
    private readonly ITemplateRepository _repository;
    private readonly ILogger<TemplateService> _logger;

    /// <summary>
    /// Constructor of an service
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="logger"></param>
    public TemplateService(ITemplateRepository repository, ILogger<TemplateService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TemplateDto>> GetAllAsync(CancellationToken ct = default)
    {
        _logger.LogDebug("Get all {name of}s", nameof(TemplateDto));
        
        var templates = await _repository.GetAllAsync(ct);

        _logger.LogDebug("Successfully received list of {name of}", nameof(TemplateDto));
        return templates.Select(TemplateMapper.ToDto).ToList();
    }

    /// <inheritdoc />
    public async Task<TemplateDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        _logger.LogDebug("Get {name of} with id: '{id}'", nameof(TemplateDto), id);
        var template = await _repository.GetByIdAsync(id, ct);
        
        if (template is null)
        {
            throw new KeyNotFoundException($"{nameof(TemplateDto)} with id: '{id}' doesn't exist");
        }
        
        _logger.LogDebug("Successfully received {name of} with id: '{id}'", nameof(TemplateDto), id);
        return template.ToDto();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TemplateDto>> GetTemplatesByEnumTypeAsync(TemplateEnumDto templateEnum, CancellationToken ct = default)
    {
        _logger.LogDebug("Get {name of} with enum state: '{state}'", nameof(TemplateDto), templateEnum);
        var templatesByEnum = await _repository.GetTemplatesByEnumType(templateEnum.ToEntity(), ct);
        
        if (templatesByEnum is null)
        {
            throw new KeyNotFoundException($"{nameof(TemplateDto)} with enum state: '{templateEnum}' doesn't exist");
        }
        
        _logger.LogDebug("Successfully received {name of} with id: '{id}'", nameof(TemplateDto), templateEnum);
        return templatesByEnum.Select(TemplateMapper.ToDto).ToList();
    }
    
    /// <inheritdoc />
    public async Task<TemplateDto> CreateAsync(CreateTemplateDto dtoToCreate, CancellationToken ct = default)
    {
        _logger.LogDebug("Create new template with Title {title}", dtoToCreate.Title);
       
        var entity = await _repository.CreateAsync(dtoToCreate.ToEntity(), ct);
        return entity.ToDto();
    }
    
    /// <inheritdoc />
    public async Task<TemplateDto> UpdateByIdAsync(Guid id, UpdateTemplateDto dtoToUpdate, CancellationToken ct = default)
    {
        _logger.LogDebug("Update {name of} with id: '{id}'", nameof(TemplateDto), id);
        var template = await _repository.GetByIdAsync(id, ct);
        
        if (template is null)
        {
            _logger.LogWarning("Impossible to confirm existence of {name of} with id: '{id}' while update", nameof(TemplateDto), id);
            throw new KeyNotFoundException($"{nameof(TemplateDto)} with id: '{id}' doesn't exist");
        }

        var templateToUpdate = dtoToUpdate.ToEntity(id);
        
        var updatedTemplate = await _repository.UpdateAsync(templateToUpdate, ct);
        if (updatedTemplate is null)
        {
            _logger.LogError("Cannot update {name of} with id: '{id}'", nameof(Template), id);
            throw new InvalidProgramException($"Cannot update {nameof(TemplateDto)} with id: '{id}'");
        }

        var result = await _repository.GetByIdAsync(id, ct);
        
        _logger.LogDebug("Successfully updated {name of} with id: '{id}'", nameof(TemplateDto), id);
        return result.ToDto();
    }

    /// <inheritdoc />
    public async Task<Guid> DeleteByIdAsync(Guid id, CancellationToken ct = default)
    {
        _logger.LogDebug("Delete {name of} with id: '{id}'", nameof(TemplateDto), id);
        await _repository.GetByIdAsync(id, ct);
        var deletedId = await _repository.DeleteByIdAsync(id, ct);

        _logger.LogDebug("Successfully delete {name of} with id: '{id}'", nameof(TemplateDto), id);
        return deletedId;
    }
}