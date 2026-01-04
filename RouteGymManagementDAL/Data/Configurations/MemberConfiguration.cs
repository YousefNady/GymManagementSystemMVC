using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RouteGymManagementDAL.Entities;

namespace RouteGymManagementDAL.Data.Configurations
{
    internal class MemberConfiguration : GymUserBaseConfiguration<Member>, IEntityTypeConfiguration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {

            builder.Property(x => x.CreatedAt)
            .HasColumnName("JoinDate")
            .HasDefaultValueSql("GETDATE()");

            base.Configure(builder);
        }
    }
}
