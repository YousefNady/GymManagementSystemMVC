using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RouteGymManagementDAL.Entities;

namespace RouteGymManagementDAL.Data.Configurations
{
    internal class CategoreConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.CategoryName)
                .HasColumnType("varchar")
                .HasMaxLength(20);
        }
    }
}
