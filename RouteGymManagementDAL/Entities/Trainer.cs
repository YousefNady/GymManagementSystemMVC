using RouteGymManagementDAL.Entities.Enums;

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
