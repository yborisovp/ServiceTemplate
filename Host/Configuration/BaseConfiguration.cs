namespace ServiceTemplate.Configuration;

/// <summary>
/// Base configuration
/// </summary>
public class BaseConfiguration
{
    public DatabaseConfiguration DatabaseConfig { get; set; } = new();
    public SwaggerConfiguration SwaggerConfig { get; set; } = new();
}
