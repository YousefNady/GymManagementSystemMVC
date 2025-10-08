using RouteGymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementDAL.Repositories.Interfaces
{
    public interface ITrainerRepository
    {

        // GetAll
        IEnumerable<Trainer> GetAll();

        // Get By Id
        Trainer? GetById(int Id); // Num Of Rows Affected

        // Add
        int Add(Trainer trainer);

        // Update
        int Update(Trainer trainer);

        int Delete(Trainer trainer);

    }
}
