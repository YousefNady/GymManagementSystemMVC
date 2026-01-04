using RouteGymManagementBLL.BusinessServices.Interfaces;
using RouteGymManagementBLL.View_Models.AnalyticsVMs;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.UnitOfWorkPattern.Interfaces;

namespace RouteGymManagementBLL.BusinessServices.Implementation
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork unitOfWork;

        public AnalyticsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public AnalyticsViewModel GetAnalyticsData()
        {
            var sessions = unitOfWork.GetRepository<Session>().GetAll();
            return new AnalyticsViewModel
            {
                ActiveMembers = unitOfWork.GetRepository<Membership>().GetAll(x => x.Status == "Active").Count(),
                TotalMembers = unitOfWork.GetRepository<Member>().GetAll().Count(),
                TotalTrainers = unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                CompletedSessions = sessions.Count(x => x.EndDate < DateTime.Now),
                OngoingSessions = sessions.Count(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now),
                UpcomingSessions = sessions.Count(x => x.StartDate > DateTime.Now)
            };
        }
    }
}
