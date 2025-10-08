using RouteGymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementDAL.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        // GetAll
        IEnumerable<Category> GetAll();

        // Get By Id
        Category? GetById(int Id); // Num Of Rows Affected

        // Add
        int Add(Category category);

        // Update
        int Update(Category category);

        int Delete(Category category);
    }
}
