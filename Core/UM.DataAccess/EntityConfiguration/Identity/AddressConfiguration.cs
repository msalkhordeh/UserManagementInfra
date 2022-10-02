using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UM.DataAccess.Entity.Identity;
using UM.DataAccess.Enum;

namespace UM.DataAccess.EntityConfiguration.Identity
{
    public class AddressConfiguration
        : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses", "Identity");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.FullAddress)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(x => x.City)
                .HasMaxLength(50);

            builder.Property(x => x.Country)
                .HasMaxLength(50);

            builder.Property(x => x.Type)
                .HasDefaultValue(AddressType.Unkown);

            builder.Property(x => x.PostalCode)
                .HasMaxLength(10);

            builder.HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId);
        }
    }
}
