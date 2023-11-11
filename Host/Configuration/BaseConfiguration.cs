namespace ServiceTemplate.Configuration;

/// <summary>
/// Конфигурация сервиса
/// </summary>
public class BaseConfiguration
{
    public DatabaseConfiguration DatabaseConfig { get; set; } = new();
    public SwaggerConfiguration SwaggerConfig { get; set; } = new();
}
