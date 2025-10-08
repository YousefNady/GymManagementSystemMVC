using RouteGymManagementDAL.Entities;
namespace RouteGymManagementDAL.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        // GetAll
        IEnumerable<Member> GetAll();

        // Get By Id
        Member? GetById(int Id); // Num Of Rows Affected

        // Add
        int Add(Member member);

        // Update
        int Update(Member member);

        int Delete(int Id); // Num Of Rows Affected

    }
}
