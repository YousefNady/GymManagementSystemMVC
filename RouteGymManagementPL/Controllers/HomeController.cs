using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteGymManagementBLL.Services.Interfaces;
using RouteGymManagementDAL.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RouteGymManagementPL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAnalyticsService analyticsService;

        public HomeController(IAnalyticsService analyticsService)
        {
            this.analyticsService = analyticsService;
        }

        public ActionResult Index()
        {
            var analyticsData = analyticsService.GetAnalyticsData();
             return View(analyticsData);           // Return View For Action With Passing Model Data

            // return View();                // Return Default View With Action Name
            // return View(model);           // Return View For Action With Passing Model Data
            // return View("Named");         // Return Specific View For Action

            //return View("Named", analyticsData); // Return Specific View For Action With Passing Model Data



        }

        //public ActionResult Trainers()
        //{
        //    var trainers = new List<Trainer>()
        //    {
        //        new Trainer() {Name = "Yousef", Phone = "01553234246" },
        //        new Trainer() {Name = "Ali", Phone = "01123546846" }
        //    };
        //    return Json(trainers); // Helper method from Controller base class
        //}

        //public ActionResult Redirect() 
        //{
        //    // when u write (https://localhost:7029/Home/Redirect)
        //    // will Redirect u to this link (https://www.linkedin.com/in/yousef-nady/)
        //    return Redirect("https://www.linkedin.com/in/yousef-nady/"); // Helper method from Controller base class
        //}

        //public ActionResult Content()
        //{
        //    //return Content("Hello From RouteGymManagementPL"); // text/plain
        //    return Content("<h1>Hello From RouteGymManagementPL</h1>"); // text/plain
        //    //return Content("<h1>Hello From RouteGymManagementPL</h1>","Text/html"); // text/html
        //}

        //public ActionResult DownloadFile()
        //{
        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "css", "site.css");
        //    var fileBytes = System.IO.File.ReadAllBytes(filePath);
        //    return File(fileBytes, "text/html", "DownloadableSite.css");
        //}
        //public ActionResult empty()
        //{
        //    return new EmptyResult();
        //}
    }
}
