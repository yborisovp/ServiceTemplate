using ServiceTemplate.DataAccess.Models.Templates;
using ServiceTemplate.DataContracts.Dtos.Templates;

namespace ServiceTemplate.Mappers.Templates;

public static class DtoToDBTemplateMapper
{
    public static Template ToEntity(this UpdateTemplateDto updateTemplateDto, Guid templateId)
    {
        return new Template
        {
            Id = templateId,
            Title = updateTemplateDto.Title,
            TemplateEnum = updateTemplateDto.TemplateEnum.ToEntity()
        };
    }
}