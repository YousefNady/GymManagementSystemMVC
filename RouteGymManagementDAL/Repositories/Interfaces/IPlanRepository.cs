using RouteGymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementDAL.Repositories.Interfaces
{
    internal interface IPlanRepository
    {
        IEnumerable<Plan> GetAll();
        Plan? GetById(int Id);
        int Update(Plan plan);

    }
}
