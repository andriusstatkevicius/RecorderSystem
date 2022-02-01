using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecorderSystem.Entities;
using RecordSystemLibrary;

namespace RecorderSystem.Pages.RegistrationPages
{
    [Authorize(Roles = "Administrator, Consultant")]
    public class StudentRegistrationModel : PageModel
    {
        [BindProperty]
        public Student RegistrationDetails { get; set; }
        public StudentRegistrationModel()
        {

        }

        public void OnGet()
        {
        }
    }
}
