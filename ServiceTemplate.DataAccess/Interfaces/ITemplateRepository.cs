using ServiceTemplate.DataAccess.Models.Templates;
using ServiceTemplate.DataAccess.Models.Templates.Enums;

namespace ServiceTemplate.DataAccess.Interfaces;

public interface ITemplateRepository : IRepository<Template, Guid>
{
    Task<IEnumerable<Template>> GetTemplatesByEnumType(TemplateEnum templateEnum, CancellationToken ct = default);
}