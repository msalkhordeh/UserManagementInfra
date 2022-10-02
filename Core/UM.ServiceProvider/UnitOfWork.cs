using UM.DataAccess.DataContext;

namespace UM.ServiceProvider
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EfCoreContext _dbcontext;
        public UnitOfWork(EfCoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public int Complete()
        {
            return _dbcontext.SaveChanges();
        }

        public async Task<int> CompleteAsync(
            CancellationToken cancellationToken = default)
        {
            return await _dbcontext.SaveChangesAsync();
        }
    }
}
