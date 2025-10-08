using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementDAL.Entities
{
    public class Member : GymUser
    {
        // JoinDate == CreatedAt(baseEntity) [Need Fluent API]



        // we will store the url of Photo In DB
        public string? Photo { get; set; }


        #region Relationships


        #region Member - HealthRecord

        public HealthRecord HealthRecord { get; set; } = null!;


        #endregion

        #region Member - Memberships

        public ICollection<Membership> Memberships { get; set; } = null!;


        #endregion

        #region Member - MemberSessions

        public ICollection<MemberSession> MemberSessions { get; set; } = null!;

        #endregion

        #endregion
    }
}
