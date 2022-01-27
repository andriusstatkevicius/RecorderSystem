using System.ComponentModel.DataAnnotations;

namespace RecorderSystem.Entities
{
    public class LoginInput
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}
