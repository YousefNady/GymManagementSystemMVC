using RouteGymManagementBLL.View_Models.BookingVMs;
using RouteGymManagementBLL.View_Models.MembershipVMs;
using RouteGymManagementBLL.View_Models.SessionVMs;

namespace RouteGymManagementBLL.BusinessServices.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<SessionViewModel> GetAllSessionsWithTrainerAndCategory();
        IEnumerable<MemberForSessionViewModel> GetAllMembersForUpcomingSession(int id);
        IEnumerable<MemberForSessionViewModel> GetAllMembersForOngoingSession(int id);
        bool CreateBooking(CreateBookingViewModel createBookingViewModel);
        IEnumerable<MemberForSelectListViewModel> GetMembersForDropdown(int id);
        bool MemberAttended(MemberAttendOrCancelViewModel model);
        bool CancelBooking(MemberAttendOrCancelViewModel model);
    }
}
