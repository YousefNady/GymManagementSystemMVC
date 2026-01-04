using RouteGymManagementDAL.Entities;

namespace RouteGymManagementDAL.Repositories.Interfaces
{
    public interface IBookingRepository : IGenericRepository<MemberSession>
    {
        IEnumerable<MemberSession> GetSessionById(int sessionId);
    }
}
