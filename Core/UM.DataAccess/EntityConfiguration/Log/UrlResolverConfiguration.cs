using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UM.DataAccess.Entity.Log;

namespace UM.DataAccess.EntityConfiguration.Log
{
    public class UrlResolverConfiguration
        : IEntityTypeConfiguration<UrlResolver>
    {
        public void Configure(EntityTypeBuilder<UrlResolver> builder)
        {
            builder.ToTable("UrlResolvers", "Log");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Host)
                .HasMaxLength(50);

            builder.Property(x => x.Path)
                .HasMaxLength(500);

            builder.Property(x => x.UserAgent)
                .HasMaxLength(300);

            builder.Property(x => x.Verb)
                .HasMaxLength(7);

            builder.Property(x => x.ContentType)
                .HasMaxLength(50);

            builder.Property(x => x.Ip)
                .HasMaxLength(15);

            builder.Property(x => x.CreateDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
