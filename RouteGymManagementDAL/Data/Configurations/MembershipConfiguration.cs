using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RouteGymManagementDAL.Entities;

namespace RouteGymManagementDAL.Data.Configurations
{
    internal class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.Property(x => x.CreatedAt)
                .HasColumnName("StartDate")
                .HasDefaultValueSql("GETDATE()");




            builder.HasKey(k => new { k.MemberId, k.PlanId });

            builder.Ignore(x => x.Id);
        }
    }
}
