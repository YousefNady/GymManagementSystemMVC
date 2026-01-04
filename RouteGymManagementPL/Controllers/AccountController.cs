using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RouteGymManagementBLL.BusinessServices.Interfaces;
using RouteGymManagementBLL.View_Models.AccountVMs;
using RouteGymManagementDAL.Entities;

namespace RouteGymManagementPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(IAccountService accountService, SignInManager<ApplicationUser> signInManager)
        {
            this.accountService = accountService;
            this.signInManager = signInManager;
        }

        #region Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = accountService.ValidateUser(model);
            if (user == null)
            {
                ModelState.AddModelError("InvalidLogin", "Invalid Email Or Password");
                return View(model);
            }

            var result = signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false).Result;
            if (result.IsNotAllowed)
            {
                ModelState.AddModelError("InvalidLogin", "Your Account Not Allowed");
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("InvalidLogin", "Your Account Is Locked Out");
            }

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
        #endregion

        #region LogOut
        [HttpPost]
        public ActionResult Logout()
        {
            signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(actionName: nameof(Login));
        }

        #endregion


        #region AccessDenied
        public ActionResult AccessDenied()
        {
            return View();
        }
        #endregion
    }
}
