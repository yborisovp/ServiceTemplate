using System.ComponentModel.DataAnnotations;
using ServiceTemplate.DataAccess.Models.Templates.Enums;

namespace ServiceTemplate.DataAccess.Models.Templates;

public class Template
{
    [Key]
    public Guid Id { get; set; }
    
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public TemplateEnum TemplateEnum { get; set; } = TemplateEnum.FirstEnumAttribute;
}