using RouteGymManagementBLL.View_Models.AccountVMs;
using RouteGymManagementDAL.Entities;

namespace RouteGymManagementBLL.BusinessServices.Interfaces
{
    public interface IAccountService
    {
        ApplicationUser? ValidateUser(LoginViewModel loginViewModel);
    }
}
