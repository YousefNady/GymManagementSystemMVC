using RouteGymManagementDAL.Data.Contexts;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.Repositories.Interfaces;

namespace RouteGymManagementDAL.Repositories.Classes
{
    public class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext context;

        public PlanRepository(GymDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Plan> GetAll() => context.Plans.ToList();

        public Plan? GetById(int Id) => context.Plans.Find(Id);

        public int Update(Plan plan)
        {
            context.Plans.Update(plan);
            return context.SaveChanges();
        }
    }
}
