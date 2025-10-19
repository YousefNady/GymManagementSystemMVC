using RouteGymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementDAL.Repositories.Interfaces
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        IEnumerable<Session> GetAllSessionWithTrainerAndCategory();
        int GetCountOfBookedSlots(int sessionId);

        Session? GetSessionSessionWithTrainerAndCategory(int sessionId);

    }
}
