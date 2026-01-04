using AutoMapper;
using RouteGymManagementBLL.Attachment_Service;
using RouteGymManagementBLL.BusinessServices.Interfaces;
using RouteGymManagementBLL.View_Models.MemberVMs;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.UnitOfWorkPattern.Interfaces;

namespace RouteGymManagementBLL.BusinessServices.Implementation
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAttachmentService _attachmentService;

        public MemberService(IUnitOfWork unitOfWork, IMapper mapper, IAttachmentService attachmentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _attachmentService = attachmentService;
        }

        public bool CreateMember(CreateMemberViewModel createdMember)
        {
            try
            {
                if (IsEmailExists(createdMember.Email) || IsPhoneExists(createdMember.Phone))
                    return false;
                var photoName = _attachmentService.Upload("members", createdMember.PhotoFile);
                if (string.IsNullOrEmpty(photoName)) return false;

                // Use AutoMapper to create Member entity
                var memberEntity = _mapper.Map<Member>(createdMember);
                memberEntity.Photo = photoName;


                _unitOfWork.GetRepository<Member>().Add(memberEntity);
                var IsCreated = _unitOfWork.SaveChanges() > 0;
                if (!IsCreated)  // Rollback photo - upload || 0 rows affected
                {
                    return _attachmentService.Delete(photoName, "members");
                }
                else
                {
                    return IsCreated;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();
            if (members is null || !members.Any())
            {
                return [];
            }

            // Use AutoMapper for basic mapping
            var memberViewModels = _mapper.Map<IEnumerable<MemberViewModel>>(members);

            // Enrich with membership data
            foreach (var viewModel in memberViewModels)
            {
                var member = members.First(m => m.Id == viewModel.Id);
                var activeMembership = _unitOfWork.GetRepository<Membership>()
                    .GetAll(x => x.MemberId == member.Id && x.Status == "Active")
                    .FirstOrDefault();

                if (activeMembership is not null)
                {
                    viewModel.MembershipStartDate = activeMembership.CreatedAt.ToShortDateString();
                    viewModel.MembershipEndDate = activeMembership.EndDate.ToShortDateString();
                    var plan = _unitOfWork.GetRepository<Plan>().GetById(activeMembership.PlanId);
                    viewModel.PlanName = plan?.Name;
                }
            }

            return memberViewModels;
        }

        public HealthRecordViewModel? GetMemberHealthRecordDetails(int memberId)
        {
            var memberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(memberId);
            if (memberHealthRecord is null)
            {
                return null;
            }

            return _mapper.Map<HealthRecordViewModel>(memberHealthRecord);
        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            if (member is null)
            {
                return null;
            }

            return _mapper.Map<MemberToUpdateViewModel>(member);
        }

        public MemberViewModel? GetMemberViewDetails(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            if (member is null)
            {
                return null;
            }

            // Use AutoMapper for basic mapping
            var viewModel = _mapper.Map<MemberViewModel>(member);

            // Enrich with additional data
            var activeMembership = _unitOfWork.GetRepository<Membership>()
                .GetAll(x => x.MemberId == memberId && x.Status == "Active")
                .FirstOrDefault();

            if (activeMembership is not null)
            {
                viewModel.MembershipStartDate = activeMembership.CreatedAt.ToShortDateString();
                viewModel.MembershipEndDate = activeMembership.EndDate.ToShortDateString();
                var plan = _unitOfWork.GetRepository<Plan>().GetById(activeMembership.PlanId);
                viewModel.PlanName = plan?.Name;
            }

            return viewModel;
        }

        public bool UpdateMemberDetails(int id, MemberToUpdateViewModel updatedMember)
        {
            try
            {
                var memberRepo = _unitOfWork.GetRepository<Member>();

                var emailExists = _unitOfWork.GetRepository<Member>()
                    .GetAll(x => x.Email == updatedMember.Email && x.Id != id);

                var phoneExists = _unitOfWork.GetRepository<Member>()
                    .GetAll(x => x.Phone == updatedMember.Phone && x.Id != id);

                if (emailExists.Any() || phoneExists.Any())
                    return false;


                var member = memberRepo.GetById(id);
                if (member is null)
                    return false;

                // Use AutoMapper to update properties
                _mapper.Map(updatedMember, member);
                member.UpdatedAt = DateTime.Now;

                memberRepo.Update(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveMember(int memberId)
        {
            try
            {
                var memberRepo = _unitOfWork.GetRepository<Member>();
                var member = memberRepo.GetById(memberId);
                if (member is null) return false;

                var memberSessionsIds = _unitOfWork.GetRepository<MemberSession>()
                    .GetAll(X => X.MemberId == memberId)
                    .Select(X => X.SessionId);     // 1 5 8

                var hasFutureSessions = _unitOfWork.GetRepository<Session>().GetAll(
                   S => memberSessionsIds.Contains(S.Id) && S.StartDate > DateTime.Now).Any();

                if (hasFutureSessions)
                    return false;


                var membershipRepo = _unitOfWork.GetRepository<Membership>();
                var memberShips = membershipRepo.GetAll(X => X.MemberId == memberId);

                if (memberShips.Any())
                {
                    foreach (var membership in memberShips)
                    {
                        membershipRepo.Delete(membership);  //Transaction
                    }
                }

                memberRepo.Delete(member); //Transaction

                var isDeleted = _unitOfWork.SaveChanges() > 0;
                if (isDeleted)
                {
                    _attachmentService.Delete(member.Photo, "members");
                }

                return isDeleted;
            }
            catch (Exception)
            {

                return false;
            }

        }

        #region Helper Methods
        private bool IsPhoneExists(string phone)
        {
            return _unitOfWork.GetRepository<Member>()
                .GetAll(x => x.Phone == phone)
                .Any();
        }

        private bool IsEmailExists(string email)
        {
            return _unitOfWork.GetRepository<Member>()
                .GetAll(x => x.Email == email)
                .Any();
        }
        #endregion
    }
}