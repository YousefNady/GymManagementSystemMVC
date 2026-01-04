using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteGymManagementBLL.BusinessServices.Interfaces;
using RouteGymManagementBLL.View_Models.PlanVMs;

namespace RouteGymManagementPL.Controllers
{
    [Authorize]
    public class PlanController : Controller
    {
        private readonly IPlanService planService;

        public PlanController(IPlanService planService)
        {
            this.planService = planService;
        }

        #region Get ALl Plans


        // Index {Get}
        public IActionResult Index()
        {
            var plans = planService.GetAllPlans();
            return View(plans);
        }


        #endregion

        #region Get Plan Details
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id";
                return RedirectToAction(nameof(Index));
            }
            var plan = planService.GetPlanById(id);
            if (plan is null)
            {
                TempData["ErrorMessage"] = "Plan Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }
        #endregion

        #region Edit Plan
        // GET: Plan/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id";
                return RedirectToAction(nameof(Index));
            }
            var plan = planService.GetPlanToUpdate(id);
            if (plan is null)
            {
                TempData["ErrorMessage"] = "Plan Not Be Updated";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);

        }

        // Edit {Post}
        // POST: Plan/Edit/5
        [HttpPost]
        public ActionResult Edit([FromRoute] int id, UpdatePlanViewModel UpdatedPlan)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("ErrorMessage", "Check Your Data Validations");
                return View(UpdatedPlan); // Return The View With The Same Data 
            }

            var result = planService.UpdatePlan(id, UpdatedPlan);
            if (result)
            {
                TempData["SuccessMessage"] = "Plan Updated Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Plan Failed To Update";
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Activate(int id)
        {
            var result = planService.ToggleStatus(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Plan Status Changed";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Change Plan Status";
            }
            return RedirectToAction(nameof(Index));

        }

        #endregion
    }
}
