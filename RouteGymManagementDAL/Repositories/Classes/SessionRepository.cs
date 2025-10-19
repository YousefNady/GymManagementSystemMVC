using Microsoft.EntityFrameworkCore;
using RouteGymManagementDAL.Data.Contexts;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementDAL.Repositories.Classes
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext dbContext;

        public SessionRepository(GymDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Session> GetAllSessionWithTrainerAndCategory()
        {
            return dbContext.Sessions.Include(x => x.SessionTrainer)
                                        .Include(x => x.SessionCategore)
                                        .ToList();  
        }

        public int GetCountOfBookedSlots(int sessionId)
        {
            return dbContext.MemberSessions.Count(x => x.SessionId == sessionId);
        }

        public Session? GetSessionSessionWithTrainerAndCategory(int sessionId)
        {
           return dbContext.Sessions.Include(x => x.SessionTrainer)
                .Include(x => x.SessionCategore)
                .FirstOrDefault(x => x.Id == sessionId); 
        }
    }
}
