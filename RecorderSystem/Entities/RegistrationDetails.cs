using System.ComponentModel.DataAnnotations;

namespace RecorderSystem.Entities
{
    public class RegistrationDetails
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
    }
}
