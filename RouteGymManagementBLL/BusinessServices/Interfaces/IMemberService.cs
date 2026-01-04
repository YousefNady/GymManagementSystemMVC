using RouteGymManagementBLL.View_Models.MemberVMs;

namespace RouteGymManagementBLL.BusinessServices.Interfaces
{
    public interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMembers();

        bool CreateMember(CreateMemberViewModel createdMember);

        MemberViewModel? GetMemberViewDetails(int MemberId);

        HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId);

        MemberToUpdateViewModel? GetMemberToUpdate(int memberId);

        bool UpdateMemberDetails(int Id, MemberToUpdateViewModel UpdatedMember);

        bool RemoveMember(int MemberId);

    }
}
