using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RouteGymManagementBLL.BusinessServices.Interfaces;
using RouteGymManagementBLL.View_Models.MembershipVMs;

namespace RouteGymManagementPL.Controllers
{
    public class MembershipController : Controller
    {
        private readonly IMembershipService _membershipService;

        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        #region Index
        public IActionResult Index()
        {
            var memberships = _membershipService.GetAllMemberships();
            return View(memberships);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            LoadDropDowns();
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateMembershipViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _membershipService.CreateMembership(model);
                if (result)
                {
                    TempData["SuccessMessage"] = "Membership Created Successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Create Membership";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Failed to Create Membership, Please Check Your Data And Try Again";
            LoadDropDowns();

            return View(model);

        }
        #endregion

        #region Delete
        public IActionResult Cancel(int id)
        {
            var result = _membershipService.DeleteMembership(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Membership Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Membership Cannot Be Deleted";
                return RedirectToAction(nameof(Index));
            }        
        }
        #endregion

        #region Helper Methods
        private void LoadDropDowns() 
        {
            var members = _membershipService.GetMembersForDropdown();
            var plans = _membershipService.GetPlansForDropdown();

            ViewBag.Members = new SelectList(members, "Id", "Name");
            ViewBag.Plans = new SelectList(plans, "Id", "Name");
        }
        #endregion
    }
}
