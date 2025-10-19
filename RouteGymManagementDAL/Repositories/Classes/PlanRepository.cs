using Microsoft.EntityFrameworkCore;
using RouteGymManagementDAL.Data.Contexts;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
