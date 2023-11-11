using Microsoft.AspNetCore.Mvc;
using ServiceTemplate.DataContracts.Dtos.Templates;
using ServiceTemplate.DataContracts.Dtos.Templates.Enums;

namespace ServiceTemplate.Interfaces;

public interface ITemplateController: IBaseController<TemplateDto, Guid, UpdateTemplateDto>
{
    Task<ActionResult<IEnumerable<TemplateDto>>> GetTemplatesByEnumType(TemplateEnumDto templateEnum, CancellationToken ct = default);
}