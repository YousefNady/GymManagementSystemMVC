using RouteGymManagementDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementDAL.Entities
{
    public class Trainer : GymUser
    {

        // HireDate == CreatedAt(baseEntity) [Need Fluent API]


        public Specialties Specialties { get; set; }


        #region Trainer - Session
        public ICollection<Session> TrainerSessions { get; set; } = null!;
        #endregion

    }
}
