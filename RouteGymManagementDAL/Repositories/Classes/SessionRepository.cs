using RouteGymManagementDAL.Data.Contexts;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace RouteGymManagementDAL.Repositories.Classes
{
    internal class SessionRepository : ISessionRepository
    {
        private readonly GymDbContext dbContext;

        public SessionRepository(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add(Session session)
        {
            dbContext.Sessions.Add(session);
            return dbContext.SaveChanges();
        }

        public int Delete(Session session)
        {
            dbContext.Sessions.Remove(session);
            return dbContext.SaveChanges();
        }

        public IEnumerable<Session> GetAll() => dbContext.Sessions.ToList();


        public Session? GetById(int Id) => dbContext.Sessions.Find(Id);


        public int Update(Session session)
        {
            dbContext.Sessions.Update(session);
            return dbContext.SaveChanges();
        }
    }
}
