using RouteGymManagementDAL.Data.Contexts;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.Repositories.Interfaces;
namespace RouteGymManagementDAL.Repositories.Classes
{
    public class MemberRepository : IMemberRepository
    {
        private readonly GymDbContext _dbContext;


        // Ask CLR To Inject Object From GymDbContext
        // DbContext Object Is Injected , Not Created Manually


        public MemberRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Member member)
        {
            _dbContext.Members.Add(member);
            return _dbContext.SaveChanges();
        }

        public int Delete(int Id)
        {
            var member = _dbContext.Members.Find(Id);
            if (member is null)
            {
                return 0;
            }
            else
            {
                _dbContext.Members.Remove(member);
            }
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Member> GetAll() => _dbContext.Members.ToList();


        public Member? GetById(int Id) => _dbContext.Members.Find(Id);

        public int Update(Member member)
        {
            _dbContext.Members.Update(member);
            return _dbContext.SaveChanges();
        }
    }
}
