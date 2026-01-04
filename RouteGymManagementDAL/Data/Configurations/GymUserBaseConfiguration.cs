using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RouteGymManagementDAL.Entities;

namespace RouteGymManagementDAL.Data.Configurations
{
    internal class GymUserBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100);


            builder.Property(x => x.Phone)
                .HasColumnType("varchar")
                .HasMaxLength(11);


            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("GymUserValidEmailCheck", "Email like '_%@_%._%'");
                tb.HasCheckConstraint("GymUserValidPhoneCheck", "Phone Like '01%' And Phone Not Like '%[^0-9]' ");
            });

            builder.HasIndex(i => i.Email).IsUnique();
            builder.HasIndex(i => i.Phone).IsUnique();

            builder.OwnsOne(x => x.Address, AddressBuilder =>
            {
                AddressBuilder.Property(x => x.Street)
                .HasColumnName("Street")
                .HasColumnType("varchar")
                .HasMaxLength(30);

                AddressBuilder.Property(x => x.City)
                .HasColumnName("City")
                .HasColumnType("varchar")
                .HasMaxLength(30);

                AddressBuilder.Property(x => x.BuildingNumber)
                .HasColumnName("BuildingNumber");

            });

        }
    }
}
