using FR.DataAccess.Context;
using ServiceTemplate.DataAccess.Context;

namespace ServiceTemplate.DataAccess.Repositories
{
    public class BaseRepository
    {
        protected IDatabaseContextFactory<DatabaseContext> ContextFactory { get; set; }
        protected IDatabaseContextFactory<IdentityDatabaseContext> IdentityContextFactory { get; set; }

        protected BaseRepository(IDatabaseContextFactory<DatabaseContext> contextFactory, IDatabaseContextFactory<IdentityDatabaseContext> identityDatabaseContext)
        {
            ContextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            IdentityContextFactory = identityDatabaseContext ?? throw new ArgumentNullException(nameof(identityDatabaseContext));
        }
    }
}
