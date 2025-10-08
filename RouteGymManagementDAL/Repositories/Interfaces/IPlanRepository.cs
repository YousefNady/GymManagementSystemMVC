using RouteGymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementDAL.Repositories.Interfaces
{
    public interface IPlanRepository
    {
        // GetAll
        IEnumerable<Plan> GetAll();

        // Get By Id
        Plan? GetById(int Id); // Num Of Rows Affected

        // Add
        int Add(Plan plan);

        // Update
        int Update(Plan plan);

        int Delete(Plan plan);
    }
}
