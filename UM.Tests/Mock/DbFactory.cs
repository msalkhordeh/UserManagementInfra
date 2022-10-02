using Microsoft.EntityFrameworkCore;
using System;
using UM.DataAccess.DataContext;

namespace UM.Tests.Mock
{
    public class DbFactory : IDisposable
    {
        public EfCoreContext Context { get; private set; }

        public DbFactory()
        {
            Context = new EfCoreContext(CreateDbContextOptions());
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }

        private DbContextOptions<EfCoreContext> CreateDbContextOptions()
        {
            var builder = new DbContextOptionsBuilder<EfCoreContext>();
            builder.UseInMemoryDatabase($"UmInMemory-{Guid.NewGuid()}");
            return builder.Options;
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
