using AutoMapper;
using RouteGymManagementBLL.BusinessServices.Interfaces;
using RouteGymManagementBLL.View_Models.PlanVMs;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.UnitOfWorkPattern.Interfaces;

namespace RouteGymManagementBLL.BusinessServices.Implementation
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PlanService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans = unitOfWork.GetRepository<Plan>().GetAll();

            if (plans is null || !plans.Any())
            {
                return [];
            }
            return mapper.Map<IEnumerable<PlanViewModel>>(plans);
        }

        public PlanViewModel? GetPlanById(int PlanId)
        {
            var plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan is null)
            {
                return null;
            }
            return mapper.Map<PlanViewModel>(plan);
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int PlanId)
        {
            var plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan is null || plan.IsActive == false || HasActiveMemberShips(PlanId))
            {
                return null;
            }
            return mapper.Map<UpdatePlanViewModel>(plan);

        }


        // soft delete
        public bool ToggleStatus(int PlanId)
        {
            try
            {
                var Repo = unitOfWork.GetRepository<Plan>();
                var Plan = Repo.GetById(PlanId);

                if (Plan is null || HasActiveMemberShips(PlanId))
                    return false;

                Plan.IsActive = Plan.IsActive == true ? false : true;
                Plan.UpdatedAt = DateTime.Now;

                Repo.Update(Plan);
                return unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }


        public bool UpdatePlan(int PlanId, UpdatePlanViewModel UpdatedPlan)
        {
            var plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan is null || HasActiveMemberShips(PlanId)) return false;

            try
            {
                // Update plan properties
                mapper.Map(UpdatedPlan, plan);
                plan.UpdatedAt = DateTime.Now;
                unitOfWork.GetRepository<Plan>().Update(plan);
                return unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }


        #region Helper

        private bool HasActiveMemberShips(int planId)
        {
            var MemberShips = unitOfWork.GetRepository<Membership>()
                .GetAll(x => x.PlanId == planId && x.Status == "Active");
            return MemberShips.Any();
        }
        #endregion  
    }
}