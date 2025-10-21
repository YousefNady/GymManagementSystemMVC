using RouteGymManagementBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RouteGymManagementBLL.ViewModels.MemberViewModels;

namespace RouteGymManagementPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService memberService;

        public MemberController(IMemberService memberService)
        {
            this.memberService = memberService;
        }

        #region Get All Members
        public ActionResult Index(int id)
        {
            var members = memberService.GetAllMembers();
            return View(members);
        }
        #endregion

        #region Get Member Data

        // Case01 ->  BaseUrl/Member/MemberDetails -> id = 0
        // Case02 ->  BaseUrl/Member/MemberDetails 1 -> id = 1

        public ActionResult MemberDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member Can Not Be Or Negative Number";
                return RedirectToAction(nameof(Index));
            }

            var member = memberService.GetMemberViewDetails(id);
            if (member is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        public ActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member Can Not Be Or Negative Number";
                return RedirectToAction(nameof(Index));
            }
            var HealthRecord = memberService.GetMemberHealthRecordDetails(id);
            if (HealthRecord is null )
            {
                TempData["ErrorMessage"] = "HealthRecord Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(HealthRecord);

        }
        #endregion

        #region Create Member

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost] // submit from the form
        public ActionResult CreateMember(CreateMemberViewModel CreatedMember)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Please Correct The Errors Below");
                return View(nameof(Create), CreatedMember);
            }

         bool result =   memberService.CreateMember(CreatedMember);
            if (result)
            {
                TempData["SuccessMessage"] = "Member Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed to Create, Check Phone And Email";
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
