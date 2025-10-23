using RouteGymManagementBLL.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementBLL.Services.Interfaces
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
