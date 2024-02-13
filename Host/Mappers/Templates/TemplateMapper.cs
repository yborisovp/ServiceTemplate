using ServiceTemplate.DataAccess.Models.Templates;
using ServiceTemplate.DataContracts.Dtos.Templates;

namespace ServiceTemplate.Mappers.Templates;

/// <summary>
/// Templates converter
/// </summary>
public static class TemplateMapper
{
    /// <summary>
    /// Convert database entity to dto
    /// </summary>
    /// <param name="entity">Database entity</param>
    /// <returns>Dto that based on databased entity</returns>
    public static TemplateDto ToDto(this Template entity)
    {
        return new TemplateDto
        {
            Id = entity.Id,
            Title = entity.Title,
            TemplateEnum = entity.TemplateEnum.ToDto()
        };
    }
    
    /// <summary>
    /// Convert database dto to entity
    /// </summary>
    /// <param name="dtoToCreate">Created dto</param>
    /// <returns></returns>
    public static Template ToEntity(this CreateTemplateDto dtoToCreate)
    {
        return new Template
        {
            Title = dtoToCreate.Title,
            TemplateEnum = dtoToCreate.TemplateEnum.ToEntity()
        };
    }
    
    /// <summary>
    /// Convert database entity to dto
    /// </summary>
    /// <param name="updatedDto">Updated entity</param>
    /// <param name="templateId">id of entity</param>
    /// <returns></returns>
    public static Template ToEntity(this UpdateTemplateDto updatedDto, Guid templateId)
    {
        return new Template
        {
            Id = templateId,
            Title = updatedDto.Title,
            TemplateEnum = updatedDto.TemplateEnum.ToEntity()
        };
    }
}