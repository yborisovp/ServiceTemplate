using Microsoft.EntityFrameworkCore;
using ServiceTemplate.DataAccess.Models.Templates;
using Toolbelt.ComponentModel.DataAnnotations;

namespace ServiceTemplate.DataAccess.Context;

public class DatabaseContext : DbContext
{
    public const string DefaultSchema = "ServiceTemplate";
    public const string DefaultMigrationHistoryTableName = "__MigrationsHistory";

    public DbSet<Template> Templates { get; set; } = null!;

    public DatabaseContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DefaultSchema);
        modelBuilder.BuildIndexesFromAnnotations();

    }
}
