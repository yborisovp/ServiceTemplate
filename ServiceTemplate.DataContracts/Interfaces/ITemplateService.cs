using ServiceTemplate.DataContracts.Dtos;
using ServiceTemplate.DataContracts.Dtos.Templates;
using ServiceTemplate.DataContracts.Dtos.Templates.Enums;

namespace ServiceTemplate.DataContracts.Interfaces;

public interface ITemplateService : IBaseService<TemplateDto, Guid, UpdateTemplateDto>
{
    Task<IEnumerable<TemplateDto>> GetTemplatesByEnumTypeAsync(TemplateEnumDto templateEnum, CancellationToken ct = default);
}