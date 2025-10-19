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
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private readonly Dictionary<Type, object> _repositories = new();
        private readonly GymDbContext _dbContext;
        private readonly ISessionRepository sessionRepository1;

        public UnitOfWork(GymDbContext dbContext, ISessionRepository sessionRepository)
        {
            _dbContext = dbContext;
            sessionRepository1 = sessionRepository;
        }

        public ISessionRepository sessionRepository { get; }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var entityType = typeof(TEntity);
            if (_repositories.TryGetValue(entityType, out var repo))
                return (IGenericRepository<TEntity>)repo;

            var newRepo = new GenericRepository<TEntity>(_dbContext);
            _repositories[entityType] = newRepo;
            return newRepo;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
