using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementBLL.ViewModels.PlanViewModels
{
    public class UpdatePlanViewModel
    {
        [Required(ErrorMessage = "Plan Name Is Required")]
        [StringLength(50,ErrorMessage ="Plan Name Must Be Less Than 51 Char")]
        public string Name { get; set; } = null!;

        [StringLength(50,MinimumLength = 5 ,ErrorMessage = "Description Must Be Between 5 and 200")]
        public string Description { get; set; } = null!;

        [Range(1,365,ErrorMessage = "Duration Days Must Be Between 1 and 365")]
        [Required(ErrorMessage = "Duration Days Name Is Required")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage = "Price Days Name Is Required")]
        [Range(0.1, 10000, ErrorMessage = "Duration Days Must Be Between 0.1 and 10000")]
        public decimal Price { get; set; }
    }
}
