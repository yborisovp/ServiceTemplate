using ServiceTemplate.DataAccess.Models.Templates.Enums;
using ServiceTemplate.DataContracts.Dtos.Templates.Enums;

namespace ServiceTemplate.Mappers.Templates;

public static class EnumMapper
{
    public static TemplateEnumDto ToDto(this TemplateEnum templateEnum)
    {
        return templateEnum switch
        {
            TemplateEnum.FirstEnumAttribute => TemplateEnumDto.FirstEnumAttribute,
            TemplateEnum.SecondEnumAttribute => TemplateEnumDto.FirstEnumAttribute,
            _ => throw new ArgumentOutOfRangeException(nameof(templateEnum), templateEnum, null)
        };
    }
    
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