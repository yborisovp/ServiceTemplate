using System.Text.Json.Serialization;

namespace ServiceTemplate.DataContracts.Dtos.Templates.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TemplateEnumDto
{
    FirstEnumAttribute,
    SecondEnumAttribute
}