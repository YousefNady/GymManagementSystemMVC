using System.ComponentModel.DataAnnotations;

namespace RouteGymManagementBLL.View_Models.AccountVMs
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)] // ***********
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }

    }
}
