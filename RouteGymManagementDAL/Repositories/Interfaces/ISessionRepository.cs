using RouteGymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace RouteGymManagementDAL.Repositories.Interfaces
{
    public interface ISessionRepository
    {
        // GetAll
        IEnumerable<Session> GetAll();

        // Get By Id
        Session? GetById(int Id); // Num Of Rows Affected

        // Add
        int Add(Session session);

        // Update
        int Update(Session session);

        int Delete(Session session);
    }
}
