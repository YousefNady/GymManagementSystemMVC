using RouteGymManagementDAL.Entities;

namespace RouteGymManagementDAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new() // new => for concrete class
    {
        IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null);

        // Get By Id
        TEntity? GetById(int Id); // Num Of Rows Affected

        // Add
        void Add(TEntity entity);

        // Update
        void Update(TEntity entity);

        void Delete(TEntity entity); // Num Of Rows Affected
    }
}
