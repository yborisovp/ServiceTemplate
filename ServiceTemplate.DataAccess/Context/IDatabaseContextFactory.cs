namespace ServiceTemplate.DataAccess.Context;

public interface IDatabaseContextFactory
{
    DatabaseContext CreateDbContext();
}
