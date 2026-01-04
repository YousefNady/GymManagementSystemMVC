using RouteGymManagementDAL.Entities;

namespace RouteGymManagementDAL.Repositories.Interfaces
{
    public interface IMembershipRepository : IGenericRepository<Membership>
    {
        // Signatures for methods
        IEnumerable<Membership> GetAllMembershipsWithMembersAndPlans(Func<Membership, bool>? filter = null);

        Membership? GetFirstOrDefault(Func<Membership, bool>? filter = null);
    }
}
