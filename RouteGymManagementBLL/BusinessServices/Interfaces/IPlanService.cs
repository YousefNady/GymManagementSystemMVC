using RouteGymManagementBLL.View_Models.PlanVMs;

namespace RouteGymManagementBLL.BusinessServices.Interfaces
{
    public interface IPlanService
    {
        IEnumerable<PlanViewModel> GetAllPlans();

        PlanViewModel? GetPlanById(int PlanId);
        UpdatePlanViewModel? GetPlanToUpdate(int PlanId);


        bool UpdatePlan(int PlanId, UpdatePlanViewModel UpdatedPlan);
        bool ToggleStatus(int PlanId);
    }
}
