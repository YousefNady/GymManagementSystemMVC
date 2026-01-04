using RouteGymManagementBLL.View_Models.SessionVMs;

namespace RouteGymManagementBLL.BusinessServices.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();

        SessionViewModel? GetSessionDetails(int sessionId);


        bool CreateSession(CreateSessionViewModel createSession);


        UpdateSessionViewModel? GetSessionToUpdate(int sessionId);

        bool UpdateSession(int sessionId, UpdateSessionViewModel updateSession);


        bool DeleteSession(int sessionId);

        IEnumerable<CategorySelectViewModel> GetAllCategoriesForDropDown();

        IEnumerable<TrainerSelectViewModel> GetAllTrainersForDropDown();
    }
}
