using AutoMapper;
using RouteGymManagementBLL.BusinessServices.Interfaces;
using RouteGymManagementBLL.View_Models.MembershipVMs;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.UnitOfWorkPattern.Interfaces;

namespace RouteGymManagementBLL.BusinessServices.Implementation
{
    public class MembershipService : IMembershipService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MembershipService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<MembershipViewModel> GetAllMemberships()
        {
            var memberships = _unitOfWork.MembershipRepository.GetAllMembershipsWithMembersAndPlans(f => f.Status.ToLower() == "active");

            var membershipViewModels = _mapper.Map<IEnumerable<MembershipViewModel>>(memberships);
            return membershipViewModels;
        }

        public bool CreateMembership(CreateMembershipViewModel model)
        {
            if (!IsMemberExist(model.MemberId) || !IsPlanExist(model.PlanId) || HasActiveMemberships(model.MemberId))
            {
                return false;
            }

            var membershipRepo = _unitOfWork.MembershipRepository;
            var membershipToCreate = _mapper.Map<Membership>(model);

            var plan = _unitOfWork.GetRepository<Plan>().GetById(model.PlanId);
            membershipToCreate.EndDate = DateTime.UtcNow.AddDays(plan!.DurationDays);

            membershipRepo.Add(membershipToCreate);
            return _unitOfWork.SaveChanges() > 0;
        }

        public IEnumerable<PlanForSelectListViewModel> GetPlansForDropdown()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll(c => c.IsActive);
            var plansSelectList = _mapper.Map<IEnumerable<PlanForSelectListViewModel>>(plans);
            return plansSelectList;
        }

        public IEnumerable<MemberForSelectListViewModel> GetMembersForDropdown()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();
            var memberSelectList = _mapper.Map<IEnumerable<MemberForSelectListViewModel>>(members);
            return memberSelectList;
        }

        public bool DeleteMembership(int memberId)
        {
            var membershipRepo = _unitOfWork.MembershipRepository;

            var membershipToDelete = membershipRepo.GetFirstOrDefault(m => m.MemberId == memberId && m.Status.ToLower() == "active");

            if (membershipToDelete is null)
            {
                return false;
            }

            membershipRepo.Delete(membershipToDelete);
            return _unitOfWork.SaveChanges() > 0;
        }

        #region Helpers Methods
        private bool IsMemberExist(int memberId)
        {
            return _unitOfWork.GetRepository<Member>().GetById(memberId) is not null;
        }

        private bool IsPlanExist(int planId)
        {
            return _unitOfWork.GetRepository<Plan>().GetById(planId) is not null;
        }

        private bool HasActiveMemberships(int memberId)
        {
            return _unitOfWork.MembershipRepository.GetAllMembershipsWithMembersAndPlans(m => m.Status.ToLower() == "Active" && m.MemberId == memberId).Any();
        }
        #endregion
    }
}
