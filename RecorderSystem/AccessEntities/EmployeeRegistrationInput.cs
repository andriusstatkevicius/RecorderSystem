using System.ComponentModel.DataAnnotations;

namespace RecorderSystem.AccessEntities
{
    public class EmployeeRegistrationInput
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "User Type")]
        public UserType UserType { get; set; }
    }
}
