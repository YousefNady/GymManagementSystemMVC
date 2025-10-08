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
    internal class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext context;

        public PlanRepository(GymDbContext context)
        {
            this.context = context;
        }
        public int Add(Plan plan)
        {
            context.Plans.Add(plan);
            return context.SaveChanges();
        }

        public int Delete(Plan plan)
        {
            context.Plans.Remove(plan);
            return context.SaveChanges();
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
