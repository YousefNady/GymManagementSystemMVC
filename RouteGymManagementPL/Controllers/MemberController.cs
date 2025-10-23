using Microsoft.AspNetCore.Mvc;
using RouteGymManagementBLL.Services.Classes;
using RouteGymManagementBLL.Services.Interfaces;
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

        #region Edit Member

        // Member/EditMember/20
        public ActionResult EditMember(int id)
        {

            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member Can Not Be Or Negative Number";
                return RedirectToAction(nameof(Index));
            }

            var member = memberService.GetMemberToUpdate(id);
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

            bool result = memberService.UpdateMemberDetails(id, memberToUpdate);
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
                TempData["ErrorMessage"] = "Id of Member Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }

            var Member = memberService.GetMemberViewDetails(id);
            if (Member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId = id;
            ViewBag.MemberId = Member.Name;

            return View();
        }


        [HttpPost]
        public ActionResult DeleteConfirmed([FromForm]int id)
        {
            var result = memberService.RemoveMember(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Member Deleted Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Can Not Be Deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
