using ServiceTemplate.DataAccess.Models.Templates;
using ServiceTemplate.DataContracts.Dtos;
using ServiceTemplate.DataContracts.Dtos.Templates;

namespace ServiceTemplate.Mappers.Templates;

/// <summary>
/// Templates Mapper fromm dto to db entity model
/// </summary>
public static class DbToDtoTemplateMapper
{
    public static TemplateDto ToDto(this Template template)
    {
        return new TemplateDto
        {
            Id = template.Id,
            Title = template.Title,
            TemplateEnum = template.TemplateEnum.ToDto()
        };
    }
}