using ServiceTemplate.DataAccess.Context;

namespace ServiceTemplate.DataAccess.Repositories
{
    public class BaseRepository
    {
        protected IDatabaseContextFactory ContextFactory { get; set; }

        protected BaseRepository(IDatabaseContextFactory contextFactory)
        {
            ContextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }
    }
}
