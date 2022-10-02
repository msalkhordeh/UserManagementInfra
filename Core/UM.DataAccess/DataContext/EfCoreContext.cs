using Microsoft.EntityFrameworkCore;
using UM.DataAccess.Entity.Identity;
using UM.DataAccess.Entity.Log;
using UM.DataAccess.EntityConfiguration.Identity;
using UM.DataAccess.EntityConfiguration.Log;

namespace UM.DataAccess.DataContext
{
    public class EfCoreContext : DbContext
    {
        public EfCoreContext(
            DbContextOptions<EfCoreContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// Represent a snapshot of User Table 
        /// </summary>
        public DbSet<User> Users => Set<User>();

        /// <summary>
        /// Represent a snapshot of Address Table 
        /// </summary>
        public DbSet<Address> Addresses => Set<Address>();

        /// <summary>
        /// Represent a snapshot of Phone Table 
        /// </summary>
        public DbSet<Phone> Phones => Set<Phone>();

        /// <summary>
        /// Represnet a Snapshot of UrlResolver Table
        /// </summary>
        public DbSet<UrlResolver> UrlResolvers => Set<UrlResolver>();

        //protected override void OnConfiguring(
        //    DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //       "Server=(localdb)\\mssqllocaldb;Database=UM;");
        //}

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new PhoneConfiguration());

            modelBuilder.ApplyConfiguration(new UrlResolverConfiguration());
        }
    }
}
