using RouteGymManagementBLL.Services.Interfaces;
using RouteGymManagementBLL.ViewModels.MemberViewModels;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.Repositories.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RouteGymManagementBLL.Services.Classes
{
    internal class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CreateMember(CreateMemberViewModel createdMember)
        {
            try
            {
                if (IsEmailExists(createdMember.Email) || IsPhoneExists(createdMember.Phone))
                    return false;

                // Use AutoMapper to create Member entity
                var member = _mapper.Map<Member>(createdMember);

                _unitOfWork.GetRepository<Member>().Add(member);
                return _unitOfWork.SaveChanges() > 0;
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

                if (IsEmailExists(updatedMember.Email) || IsPhoneExists(updatedMember.Phone))
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
            var memberRepo = _unitOfWork.GetRepository<Member>();
            var member = memberRepo.GetById(memberId);

            if (member is null)
                return false;

            var hasActiveMemberSessions = _unitOfWork.GetRepository<MemberSession>()
                .GetAll(x => x.MemberId == memberId && x.Session.StartDate > DateTime.Now)
                .Any();

            if (hasActiveMemberSessions)
                return false;

            var membershipRepo = _unitOfWork.GetRepository<Membership>();
            var memberships = membershipRepo.GetAll(x => x.MemberId == memberId);

            try
            {
                if (memberships.Any())
                {
                    foreach (var membership in memberships)
                    {
                        membershipRepo.Delete(membership);
                    }
                }

                memberRepo.Delete(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
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