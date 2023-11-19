using Microsoft.AspNetCore.Mvc;
using ServiceTemplate.DataContracts.Dtos.Templates;
using ServiceTemplate.DataContracts.Dtos.Templates.Enums;

namespace ServiceTemplate.Interfaces;

/// <summary>
/// Controller to grant access to Templates
/// </summary>
public interface ITemplateController: IBaseController<TemplateDto, Guid, UpdateTemplateDto>
{
    /// <summary>
    /// Get template by enum type
    /// </summary>
    /// <param name="templateEnum">Enum type</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>List of templates with provided enum type</returns>
    Task<ActionResult<IEnumerable<TemplateDto>>> GetTemplatesByEnumType(TemplateEnumDto templateEnum, CancellationToken ct = default);
}