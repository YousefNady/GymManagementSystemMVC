using RouteGymManagementDAL.Data.Contexts;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.Repositories.Classes;
using RouteGymManagementDAL.Repositories.Interfaces;

public class UnitOfWork : IUnitOfWork
{
    private readonly Dictionary<Type, object> _repositories = new();
    private readonly GymDbContext _dbContext;

    public ISessionRepository sessionRepository { get; }

    public UnitOfWork(GymDbContext dbContext, ISessionRepository sessionRepository)
    {
        _dbContext = dbContext;
        this.sessionRepository = sessionRepository; 
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
