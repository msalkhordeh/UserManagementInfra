using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UM.DataAccess.Entity.Identity;
using UM.DataAccess.Enum;

namespace UM.DataAccess.EntityConfiguration.Identity
{
    public class UserConfiguration
        : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "Identity");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Username)
                .HasMaxLength(75)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Role)
                .HasDefaultValue(UserRole.Public);

            builder.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(50);

            builder.Property(x => x.NationalCode)
                .HasMaxLength(10)
                .IsFixedLength(true);

            builder.Property(x => x.Gender)
                .HasDefaultValue(Gender.NoInterested);

            builder.Property(x => x.Status)
                .HasDefaultValue(UserStatus.Active);

            builder.HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder.HasMany(u => u.Phones)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);
        }
    }
}
