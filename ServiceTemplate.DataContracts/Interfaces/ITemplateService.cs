using ServiceTemplate.DataContracts.Dtos;
using ServiceTemplate.DataContracts.Dtos.Templates;
using ServiceTemplate.DataContracts.Dtos.Templates.Enums;

namespace ServiceTemplate.DataContracts.Interfaces;

public interface ITemplateService : IBaseService<TemplateDto, Guid, UpdateTemplateDto>
{
    
    /// <summary>
    /// Get template by enum type
    /// </summary>
    /// <param name="templateEnum">Enum type</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>List of templates with provided enum type</returns>
    /// <exception cref="KeyNotFoundException">If template doesn't exists, trows an error of type KeyNotFound</exception>
    Task<IEnumerable<TemplateDto>> GetTemplatesByEnumTypeAsync(TemplateEnumDto templateEnum, CancellationToken ct = default);
}