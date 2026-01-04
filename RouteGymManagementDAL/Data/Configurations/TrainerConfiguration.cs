using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RouteGymManagementDAL.Entities;

namespace RouteGymManagementDAL.Data.Configurations
{
    internal class TrainerConfiguration : GymUserBaseConfiguration<Trainer>, IEntityTypeConfiguration<Trainer>
    {
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(x => x.CreatedAt)
                .HasColumnName("HireDate")
                .HasDefaultValueSql("GETDATE()");

            base.Configure(builder);
        }
    }
}
