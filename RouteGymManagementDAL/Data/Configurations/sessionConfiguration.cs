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
    internal class sessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("SessionCapacityCheck", "Capacity between 1 and 25");
                tb.HasCheckConstraint("SessionEndDateCheck", "EndDate > StartDate");
            });

            builder.HasOne(x => x.SessionCategore)
                .WithMany(x => x.Sessions)
                .HasForeignKey(x => x.CategoryId);


            builder.HasOne(x => x.SessionTrainer)
               .WithMany(x => x.TrainerSessions)
               .HasForeignKey(x => x.TrainerId);
        }
    }
}
