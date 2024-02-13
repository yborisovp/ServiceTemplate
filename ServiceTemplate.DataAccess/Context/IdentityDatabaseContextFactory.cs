using FR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace ServiceTemplate.DataAccess.Context;

public class IdentityDatabaseContextFactory: IDatabaseContextFactory<IdentityDatabaseContext>
{
    public IdentityDatabaseContextFactory(string connectionString)
    {
        ArgumentNullException.ThrowIfNull(connectionString);
        ConnectionStringProvider = () => connectionString;
    }
    
    public Func<string> ConnectionStringProvider { get; }
    
    public IdentityDatabaseContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<IdentityDatabaseContext>();
        optionsBuilder.UseNpgsql($"{ConnectionStringProvider()}",
            x => x.MigrationsHistoryTable(
                IdentityDatabaseContext.DefaultMigrationHistoryTableName,
                IdentityDatabaseContext.DefaultSchema
            ));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        return new IdentityDatabaseContext(optionsBuilder.Options);
    }
    
    public static IdentityDatabaseContext CreateDbContext(DbContextOptionsBuilder<IdentityDatabaseContext> optionsBuilder, string connectionString)
    {
        optionsBuilder.UseNpgsql(connectionString,
            x => x.MigrationsHistoryTable(
                IdentityDatabaseContext.DefaultMigrationHistoryTableName,
                IdentityDatabaseContext.DefaultSchema
            ));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        return new IdentityDatabaseContext(optionsBuilder.Options);
    }
}