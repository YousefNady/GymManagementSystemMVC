using Microsoft.EntityFrameworkCore;
using RouteGymManagementDAL.Data.Contexts;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.Repositories.Interfaces;

namespace RouteGymManagementDAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {


        private readonly GymDbContext _dbContext;

        // Ask CLR To Inject Object From GymDbContext
        // DbContext Object Is Injected , Not Created Manually

        public GenericRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
        {
            if (condition is null)
            {
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();
            }
            else
            {
                return _dbContext.Set<TEntity>().AsNoTracking().Where(condition).ToList();

            }
        }

        public TEntity? GetById(int Id)
        {
            return _dbContext.Set<TEntity>().Find(Id);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }
    }
}
