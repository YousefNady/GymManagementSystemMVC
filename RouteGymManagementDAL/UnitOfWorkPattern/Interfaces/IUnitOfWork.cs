using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.Repositories.Interfaces;

namespace RouteGymManagementDAL.UnitOfWorkPattern.Interfaces
{
    public interface IUnitOfWork
    {
        ISessionRepository SessionRepository { get; }
        IMembershipRepository MembershipRepository { get; }
        IBookingRepository BookingRepository { get; }

        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new();
        int SaveChanges();
    }
}
