using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementBLL.ViewModels.TrainerViewModels
{
    public class TrainerToUpdateViewModel
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int BulidingNumber { get; set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Specialties { get; set; } = null!;


    }
}
