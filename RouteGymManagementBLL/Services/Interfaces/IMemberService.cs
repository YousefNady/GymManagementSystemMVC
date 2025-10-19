using RouteGymManagementBLL.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementBLL.Services.Interfaces
{
    internal interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMembers();

        bool CreateMember(CreateMemberViewModel createdMember);

        MemberViewModel? GetMemberViewDetails(int MemberId);

        HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId);

        MemberToUpdateViewModel? GetMemberToUpdate(int memberId);

        bool UpdateMemberDetails(int Id,MemberToUpdateViewModel UpdatedMember);

        bool RemoveMember(int MemberId);

    }
}
