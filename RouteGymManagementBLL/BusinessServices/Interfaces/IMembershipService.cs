using RouteGymManagementBLL.View_Models.MembershipVMs;

namespace RouteGymManagementBLL.BusinessServices.Interfaces
{
    public interface IMembershipService
    {
        IEnumerable<MembershipViewModel> GetAllMemberships();
        IEnumerable<PlanForSelectListViewModel> GetPlansForDropdown();
        IEnumerable<MemberForSelectListViewModel> GetMembersForDropdown();
        bool CreateMembership(CreateMembershipViewModel model);
        bool DeleteMembership(int memberId);
    }
}
