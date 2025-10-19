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
    internal class MemberSessionConfiguration : IEntityTypeConfiguration<MemberSession>
    {
        public void Configure(EntityTypeBuilder<MemberSession> builder)
        {
            builder.HasKey(x => new { x.MemberId, x.SessionId });
            builder.Ignore(x => x.Id);
            builder.Property(x => x.CreatedAt).HasColumnName("BookingDate").HasDefaultValueSql("GETDATE()");
        }
    }
}
