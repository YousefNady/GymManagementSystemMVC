using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RouteGymManagementBLL.Services.Classes;
using RouteGymManagementBLL.Services.Interfaces;
using RouteGymManagementBLL.ViewModels.TrainerViewModels;

namespace RouteGymManagementPL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]

    public class TrainerController : Controller
    {
        private readonly ITrainerService trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            this.trainerService = trainerService;
        }

        #region Get All Trainers
        public ActionResult Index()
        {
            var trainers = trainerService.GetAllTrainers();
            return View(trainers);
        }
        #endregion

        #region Get Trainer Data (Details)
        // GET: Trainer/Details

        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of trainer Can Not Be Or Negative Number";
                return RedirectToAction(nameof(Index));
            }

            var trainer = trainerService.GetTrainerViewDetails(id);
            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(trainer);
        }


        #endregion

        #region Create Trainer


        // GET: Trainer/Create
        // to display View inside it a form to create a new Trainer
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trainer/Create
        [HttpPost]
        public ActionResult CreateTrainer(CreateTrainerViewModel createdTrainer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissing", "Check Missing Fields");
                return View(nameof(Index), createdTrainer);
            }

            var result = trainerService.CreateTrainer(createdTrainer);
            if(result)
            {
                TempData["SuccessMessage"] = "Trainer Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Failed to Create, Check Phone And Email";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edit Trainer

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of trainer Can Not Be Or Negative Number";
                return RedirectToAction(nameof(Index));
            }

            var trainer = trainerService.GetTrainerViewDetails(id);
            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(trainer);
        }


        [HttpPost]
        public ActionResult Edit (CreateTrainerViewModel createdTrainer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissing", "Check Missing Fields");
                return View(nameof(Index), createdTrainer);
            }

            var result = trainerService.CreateTrainer(createdTrainer);
            if (result)
            {
                TempData["SuccessMessage"] = "Trainer Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Failed to Create, Check Phone And Email";
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Trainer/Edit/5
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, UpdateTrainerViewModel trainerEdited)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Check Missing Fields");
                return View(trainerEdited);
            }

            var result = trainerService.UpdateTrainerDetails(id, trainerEdited);

            if (result)
            {
                TempData[ "SuccessMessage"] = "Trainer updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update trainer.";
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion


        #region Remove Trainer

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id";
                return RedirectToAction(nameof(Index));
            }

            var trainer = trainerService.GetTrainerViewDetails(id);
            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer not found";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.TrainerId = trainer.Id;
            return View();
        }


        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = trainerService.RemoveTrainer(id);

            if (result)
            {
                TempData["SuccessMessage"] = "Trainer deleted successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete trainer";
            }

            return RedirectToAction(nameof(Index));
        }


        #endregion
    }
}
