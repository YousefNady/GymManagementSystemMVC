using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RouteGymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementDAL.Data.Configurations
{
    internal class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.ToTable("Members")
                .HasKey(x => x.Id); // not needed

            builder.HasOne(x => x.Member)
                .WithOne(x => x.HealthRecord)
                .HasForeignKey<HealthRecord>(x => x.Id);

            builder.Ignore(x => x.CreatedAt);
            builder.Ignore(x => x.UpdatedAt);

        }
    }
}
