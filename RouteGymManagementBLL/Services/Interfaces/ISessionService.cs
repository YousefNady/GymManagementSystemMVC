using GymManagementSystemBLL.ViewModels.SessionViewModels;
using RouteGymManagementBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementBLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionById(int sessionId);
        bool CreateSession(CreateSessionViewModel createdSession);
        UpdateSessionViewModel? GetSessionToUpdate(int sessionId);
        bool UpdateSession (UpdateSessionViewModel updateSession, int sessionId);
        bool RemoveSession(int sessionId);

        IEnumerable<TrainerSelectViewModel> GetAllTrainersForDropDown();
        IEnumerable<CategorySelectViewModel> GetAllCategoryForDropDown();

    }
}
