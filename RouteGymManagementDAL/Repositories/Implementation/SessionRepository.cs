using Microsoft.EntityFrameworkCore;
using RouteGymManagementDAL.Data.Contexts;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.Repositories.Interfaces;

namespace RouteGymManagementDAL.Repositories.Classes
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext _dbContext;

        public SessionRepository(GymDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Session> GetAllWithCategoryAndTrainer()
        {
            return _dbContext.Sessions
                 .Include(X => X.Category)
                 .Include(X => X.Trainer)
                 .ToList();
        }

        public Session? GetByIdWithTrainerAndCategory(int sessionId)
        {
            return _dbContext.Sessions
                .Include(X => X.Category)
                .Include(X => X.Trainer).FirstOrDefault(X => X.Id == sessionId);
        }

        public int GetCountOfBookedSlots(int sessionId)
        {
            return _dbContext.MemberSessions.Count(X => X.SessionId == sessionId);
        }
    }
}
