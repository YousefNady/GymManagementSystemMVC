using RouteGymManagementDAL.Entities;

namespace RouteGymManagementDAL.Repositories.Interfaces
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        IEnumerable<Session> GetAllWithCategoryAndTrainer();

        int GetCountOfBookedSlots(int sessionId);

        Session? GetByIdWithTrainerAndCategory(int sessionId);

    }
}
