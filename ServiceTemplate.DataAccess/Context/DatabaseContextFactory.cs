using Microsoft.EntityFrameworkCore;

namespace ServiceTemplate.DataAccess.Context;

public class DatabaseContextFactory : IDatabaseContextFactory
{
    private Func<string> ConnectionStringProvider { get; }

    public DatabaseContextFactory(string connectionString)
    {
        if (connectionString == null)
        {
            throw new ArgumentNullException(nameof(connectionString));
        }
        ConnectionStringProvider = () => connectionString;
    }
    
    public DatabaseContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder.UseNpgsql($"{ConnectionStringProvider()}",
            x => x.MigrationsHistoryTable(
                DatabaseContext.DefaultMigrationHistoryTableName,
                DatabaseContext.DefaultSchema
            ));
        optionsBuilder.UseCamelCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        return new DatabaseContext(optionsBuilder.Options);
    }
    
    public static DatabaseContext CreateDbContext(DbContextOptionsBuilder optionsBuilder, string connectionString)
    {
        optionsBuilder.UseNpgsql(connectionString,
            x => x.MigrationsHistoryTable(
                DatabaseContext.DefaultMigrationHistoryTableName,
                DatabaseContext.DefaultSchema
            ));
        optionsBuilder.UseCamelCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        return new DatabaseContext(optionsBuilder.Options);
    }
}
