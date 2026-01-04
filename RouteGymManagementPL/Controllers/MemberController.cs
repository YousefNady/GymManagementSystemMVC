using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteGymManagementBLL.BusinessServices.Interfaces;
using RouteGymManagementBLL.View_Models.MemberVMs;

namespace RouteGymManagementPL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        #region Get All Members
        public ActionResult Index(int id)
        {
            var members = _memberService.GetAllMembers();
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

            var member = _memberService.GetMemberViewDetails(id);
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
            var HealthRecord = _memberService.GetMemberHealthRecordDetails(id);
            if (HealthRecord is null)
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

            bool result = _memberService.CreateMember(CreatedMember);
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

        #region Edit Member

        // Member/EditMember/20
        public ActionResult EditMember(int id)
        {

            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member Can Not Be Or Negative Number";
                return RedirectToAction(nameof(Index));
            }

            var member = _memberService.GetMemberToUpdate(id);
            if (member is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        public ActionResult EditMember([FromRoute] int id, MemberToUpdateViewModel memberToUpdate)
        {

            if (!ModelState.IsValid)
            {
                return View(memberToUpdate);
            }

            bool result = _memberService.UpdateMemberDetails(id, memberToUpdate);
            if (result)
            {
                TempData["SuccessMessage"] = "Member Update Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed to Updated";
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete Member

        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cannot Be negative or Zero";
                return RedirectToAction(nameof(Index));
            }

            var member = _memberService.GetMemberViewDetails(id);
            if (member is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId = id;
            ViewBag.MemberName = member.Name;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = _memberService.RemoveMember(id);

            if (result)
            {
                TempData["SuccessMessage"] = "Member Deleted Succesfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed to Delete";
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
