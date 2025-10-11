using Microsoft.EntityFrameworkCore;
using RouteGymManagementDAL.Data.Contexts;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementDAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {


        private readonly GymDbContext dbContext;

        // Ask CLR To Inject Object From GymDbContext
        // DbContext Object Is Injected , Not Created Manually

        public GenericRepository(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        public void Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }


        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
        {
            if (condition is  null)
            {
                return dbContext.Set<TEntity>().AsNoTracking().ToList();
            }
            else
            {
                return dbContext.Set<TEntity>().AsNoTracking().Where(condition).ToList();

            }
        }

        public TEntity? GetById(int Id)
        {
            return dbContext.Set<TEntity>().Find(Id);
        }

        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }
    }
}
