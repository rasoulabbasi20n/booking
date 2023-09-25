using Framework.Application;

namespace Framework.EF
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        public Task Commit(CancellationToken cancellationToken)
        {
        }
    }
}