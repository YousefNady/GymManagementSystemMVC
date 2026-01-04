using Microsoft.EntityFrameworkCore;
using RouteGymManagementDAL.Data.Contexts;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.Repositories.Classes;
using RouteGymManagementDAL.Repositories.Interfaces;

namespace RouteGymManagementDAL.Repositories.Implementation
{
    public class BookingRepository : GenericRepository<MemberSession>, IBookingRepository
    {
        private readonly GymDbContext _dbContext;

        public BookingRepository(GymDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<MemberSession> GetSessionById(int sessionId)
        {
            return _dbContext.MemberSessions.Where(ms => ms.SessionId == sessionId)
                   .Include(ms => ms.Member)
                   .ToList();
        }
    }
}
