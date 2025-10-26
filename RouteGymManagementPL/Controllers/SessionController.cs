using GymManagementSystemBLL.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RouteGymManagementBLL.Services.Classes;
using RouteGymManagementBLL.Services.Interfaces;
using RouteGymManagementDAL.Entities;

namespace RouteGymManagementPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService sessionService;

        public SessionController( ISessionService sessionService )
        {
            this.sessionService = sessionService;
        }

        #region Get All Sessions
        public ActionResult Index()
        {
            var sessions = sessionService.GetAllSessions();
            return View(sessions);
        }
        #endregion


        #region Session Details
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction(nameof(Index));
            }

            var Session = sessionService.GetSessionById(id);
            if (Session is null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(Session);
        }
        #endregion

        #region Create Session
        public ActionResult Create()
        {
            LoadDropDownsForCategories();
            LoadDropDownsForTrainers();

            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateSessionViewModel createdSession)
        {
            if (!ModelState.IsValid)
            {
                LoadDropDownsForCategories();
                LoadDropDownsForTrainers();
                return View(createdSession);
            }
            var result = sessionService.CreateSession(createdSession);
            if(result)
            {
                TempData["SuccessMessage"] = "Session Created";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Create Session";
                LoadDropDownsForCategories();
                LoadDropDownsForTrainers();
                return View(createdSession);
            }
        }
        #endregion

        #region Edit Sessions
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction(nameof(Index));
            }

            var session = sessionService.GetSessionToUpdate(id);
            if (session is null)
            {
                TempData["ErrorMessage"] = "Session Can Not Be Updated";
                return RedirectToAction(nameof(Index));
            }
            LoadDropDownsForTrainers();
            return View(session);
        }
        [HttpPost]
        public ActionResult Edit([FromRoute] int id, UpdateSessionViewModel updatedSession)
        {
            if (!ModelState.IsValid)
            {
                LoadDropDownsForTrainers();
                return View( updatedSession);
            }

            var result = sessionService.UpdateSession(updatedSession, id);
            if (result)
            {
                TempData["SuccessMessage"] = "Session Updated";
            }
            else
            {
                TempData["ErrorMessage"] = "Session Failed to updated";
            }

            return RedirectToAction( nameof(Index));
        }




        #endregion

        #region Remove Session
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction(nameof(Index));
            }

            var session = sessionService.GetSessionById(id);
            if (session is null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.SessionId = session.Id;  // to send it to view 
            // 34an ytb3t el id fel form bta3t el delete (Yb3to to The Next Request)
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
           var result = sessionService.RemoveSession(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Session Deleted";
            }
            else
            {
                TempData["ErrorMessage"] = "Session Cannot be Deleted";
            }
            return RedirectToAction( nameof(Index));
        }
        #endregion


        #region HelperMethods
        private void LoadDropDownsForTrainers()
        {
            var Trainers = sessionService.GetAllTrainersForDropDown();
            ViewBag.Trainers = new SelectList(Trainers, "Id", "Name");
        }

        private void LoadDropDownsForCategories()
        {
            var Categories = sessionService.GetAllCategoryForDropDown();
            ViewBag.Categories = new SelectList(Categories, "Id", "Name");
        }
        #endregion


    }
}
