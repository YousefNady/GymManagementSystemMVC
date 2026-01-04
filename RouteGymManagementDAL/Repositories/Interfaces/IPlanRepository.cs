using RouteGymManagementDAL.Entities;

namespace RouteGymManagementDAL.Repositories.Interfaces
{
    public interface IPlanRepository
    {
        IEnumerable<Plan> GetAll();
        Plan? GetById(int Id);
        int Update(Plan plan);

    }
}
