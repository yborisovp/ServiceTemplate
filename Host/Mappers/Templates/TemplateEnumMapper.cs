using ServiceTemplate.DataAccess.Models.Templates.Enums;
using ServiceTemplate.DataContracts.Dtos.Templates.Enums;

namespace ServiceTemplate.Mappers.Templates;

/// <summary>
/// Mapping Template enum types to dto and reverse
/// </summary>
public static class TemplateEnumMapper
{
    /// <summary>
    /// Mapping db enum to dto enum
    /// </summary>
    /// <param name="templateEnum">Enum type</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException">If values doesn't exists provides an mapping error</exception>
    public static TemplateEnumDto ToDto(this TemplateEnum templateEnum)
    {
        return templateEnum switch
        {
            TemplateEnum.FirstEnumAttribute => TemplateEnumDto.FirstEnumAttribute,
            TemplateEnum.SecondEnumAttribute => TemplateEnumDto.FirstEnumAttribute,
            _ => throw new ArgumentOutOfRangeException(nameof(templateEnum), templateEnum, null)
        };
    }
    
    /// <summary>
    /// Mapping dto enum to db enum
    /// </summary>
    /// <param name="templateEnum">DTO enum type</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException">If values doesn't exists provides an mapping error</exception>
    public static TemplateEnum ToEntity(this TemplateEnumDto templateEnum)
    {
        return templateEnum switch
        {
            TemplateEnumDto.FirstEnumAttribute => TemplateEnum.FirstEnumAttribute,
            TemplateEnumDto.SecondEnumAttribute => TemplateEnum.FirstEnumAttribute,
            _ => throw new ArgumentOutOfRangeException(nameof(templateEnum), templateEnum, null)
        };
    }
}