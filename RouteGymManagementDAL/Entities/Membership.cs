using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementDAL.Entities
{
    public class Membership : BaseEntity
    {

        // StartDate == CreatedAt (BaseEntity) 

        public DateTime EndDate { get; set; }


        // readOnly Property

        public string Status
        {
            get
            {
                if (EndDate <= DateTime.Now)
                {
                    return "Expird";
                }
                else
                {
                    return "Active";
                }
            }
        }


        public int MemberId { get; set; }

        public Member Member { get; set; } = null!;

        public int PlanId { get; set; }
        public Plan Plan { get; set; } = null!;

    }
}
