using Microsoft.AspNetCore.Http;
using RouteGymManagementDAL.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace RouteGymManagementBLL.View_Models.MemberVMs
{
    public class CreateMemberViewModel
    {
        [Required(ErrorMessage = "Profile Photo Is Required")]
        [Display(Name = "Profile Photo")]
        public IFormFile PhotoFile { get; set; } = null!;

        [Required(ErrorMessage = "Name Is Required")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Name Must Be Between 2 and 20 chars")]
        [RegularExpression(@"[a-zA-Z\s]+$", ErrorMessage = "Name Can Contain Only Letters And Spaces")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")] //App Validation
        [DataType(DataType.EmailAddress)] //UI Hint
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email must be between 5 and 100")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone Is Required")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone Number Must Be Valid Egyption PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Date Of Birth Is Required")]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender Is Required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "BuildingNumber Is Required")]
        [Range(1, 9000, ErrorMessage = "Building Number Must Be Between 1 and 9000")]
        public int BuildingNumber { get; set; }


        [Required(ErrorMessage = "Street Is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street Must Be Between 2 and 30 Chars")]
        [RegularExpression(@"^[a-zA-Z0-9\u0600-\u06FF\s\-,\.]+$",ErrorMessage = "Street contains invalid characters")]
        public string Street { get; set; } = null!;


        [Required(ErrorMessage = "City Is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City Must Be Between 2 and 30 Chars")]
        [RegularExpression(@"[a-zA-Z\s]+$", ErrorMessage = "City Can Contain Only Letters And Spaces")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Health Record Is Required")]
        public HealthRecordViewModel HealthRecordViewModel { get; set; } = null!;



    }
}
