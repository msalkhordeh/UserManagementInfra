using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UM.DataAccess.Entity.Identity;
using UM.DataAccess.Enum;

namespace UM.DataAccess.EntityConfiguration.Identity
{
    public class PhoneConfiguration
        : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.ToTable("Phones", "Identity");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Type)
                .HasDefaultValue(PhoneType.Uknown);

            builder.Property(p => p.Number)
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(p => p.CountryCode)
                .HasMaxLength(10)
                .IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(u => u.Phones)
                .HasForeignKey(p => p.UserId);
        }
    }
}
