using RouteGymManagementDAL.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace RouteGymManagementBLL.View_Models.TrainerVMs
{
    public class CreateTrainerViewModel
    {
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain letters or spaces only")]
        public string Name { get; set; } = null!;


        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")] //App Validation
        [DataType(DataType.EmailAddress)] //UI Hint
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email must be between 5 and 100")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone is Required")]
        [Phone(ErrorMessage = "Invalid Phone Format")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$")]

        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "DateofBirth is Required")]
        [DataType(DataType.Date)]

        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Building Number is Required")]
        [Range(1, 9000, ErrorMessage = "Building Number between 1 and 9000")]

        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "City is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City between 2 and 30")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City must contain letters or spaces only")]

        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Street is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street between 2 and 30")]
        [RegularExpression(@"^[a-zA-Z0-9\u0600-\u06FF\s\-,\.]+$",ErrorMessage = "Street contains invalid characters")]
        public string Street { get; set; } = null!;


        [Required(ErrorMessage = "Specialization is Required")]
        [EnumDataType(typeof(Specialties))]
        public Specialties Specialties { get; set; }


    }
}
