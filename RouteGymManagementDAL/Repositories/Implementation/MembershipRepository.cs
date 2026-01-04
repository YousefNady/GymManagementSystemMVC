using Microsoft.EntityFrameworkCore;
using RouteGymManagementDAL.Data.Contexts;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.Repositories.Classes;
using RouteGymManagementDAL.Repositories.Interfaces;

namespace RouteGymManagementDAL.Repositories.Implementation
{
    public class MembershipRepository : GenericRepository<Membership>, IMembershipRepository
    {
        private readonly GymDbContext _dbContext;

        public MembershipRepository(GymDbContext dbContext) : base(dbContext) // to chain the constructor of the base class || send dbContext to GenericRepository
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Membership> GetAllMembershipsWithMembersAndPlans(Func<Membership, bool>? filter = null)
        {
            var memberships = _dbContext.Memberships
                .Include(m => m.Member)
                .Include(p => p.Plan)
                .Where(filter ?? (_ => true)); // If filter is null, return all memberships || _ is a type of Membership
                                               // _ also Means any membership you will find it will be true (matching the condition)
            return memberships;
        }

        public Membership? GetFirstOrDefault(Func<Membership, bool>? filter = null)
        {
            var membership = _dbContext.Memberships.FirstOrDefault(filter ?? (_ => true));
            return membership; // filtering in the database instead of in memory then filter it in memory
        }
    }
}
