namespace ServiceTemplate.Configuration;

/// <summary>
/// Настройки swagger
/// </summary>
public class SwaggerConfiguration
{
    /// <summary>
    /// Отключить swagger, например для окружения вне dev
    /// </summary>
    public bool IsEnabled { get; set; } = false;
}
