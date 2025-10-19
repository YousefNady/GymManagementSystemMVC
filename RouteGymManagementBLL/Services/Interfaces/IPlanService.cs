using RouteGymManagementBLL.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementBLL.Services.Interfaces
{
    internal interface IPlanService
    {
        IEnumerable<PlanViewModel> GetAllPlans();

        PlanViewModel? GetPlanById(int PlanId);
        UpdatePlanViewModel? GetPlanToUpdate(int PlanId);


        bool UpdatePlan(int PlanId, PlanViewModel Updatedplan);
        bool ToggleStatus(int PlanId);
    }
}
