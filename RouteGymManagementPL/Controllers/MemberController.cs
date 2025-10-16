using Microsoft.AspNetCore.Mvc;

namespace RouteGymManagementPL.Controllers
{
    public class MemberController : Controller
    {
        public ActionResult Index(int id)
        {
            //return RedirectToAction("GetMembers");
            return RedirectToRoute("Trainers",new { action = "GetTrainers" });

        }

        public ActionResult GetMembers()
        {
            return View();
        }

        public ActionResult CreateMember()
        {
            return View();
        }
    }
}
