using ServiceTemplate.DataAccess.Models.Templates;
using ServiceTemplate.DataContracts.Dtos.Templates;

namespace ServiceTemplate.Mappers.Templates;

/// <summary>
/// Templates Mapper fromm db entity model to dto
/// </summary>
public static class DtoToDbTemplateMapper
{
    /// <summary>
    /// Convert database template to dto
    /// </summary>
    /// <param name="updatedTemplateDto">Updated entity</param>
    /// <param name="templateId">id of template</param>
    /// <returns></returns>
    public static Template ToEntity(this UpdateTemplateDto updatedTemplateDto, Guid templateId)
    {
        return new Template
        {
            Id = templateId,
            Title = updatedTemplateDto.Title,
            TemplateEnum = updatedTemplateDto.TemplateEnum.ToEntity()
        };
    }
}