using Microsoft.AspNetCore.Identity;
using RouteGymManagementBLL.BusinessServices.Interfaces;
using RouteGymManagementBLL.View_Models.AccountVMs;
using RouteGymManagementDAL.Entities;

namespace RouteGymManagementBLL.BusinessServices.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public ApplicationUser? ValidateUser(LoginViewModel loginViewModel)
        {
            var User = userManager.FindByEmailAsync(loginViewModel.Email).Result;

            if (User is null) return null;

            var IsPasswordValid = userManager.CheckPasswordAsync(User, loginViewModel.Password).Result;

            return IsPasswordValid ? User : null;

        }
    }
}
