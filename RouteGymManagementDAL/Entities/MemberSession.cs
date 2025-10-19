using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace RouteGymManagementDAL.Entities
{
    public class MemberSession : BaseEntity
    {

        // booking date == CreatedAt (BaseEntity) 

        public bool IsAttended { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public int SessionId { get; set; }

        public Session Session { get; set; } = null!;


    }
}
