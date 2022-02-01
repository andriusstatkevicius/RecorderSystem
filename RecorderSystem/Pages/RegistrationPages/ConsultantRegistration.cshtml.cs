using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecorderSystem.Entities;

namespace RecorderSystem.Pages.RegistrationPages
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ConsultantRegistrationModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public EmployeeRegistrationInput RegistrationDetails { get; set; }

        public ConsultantRegistrationModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _userManager.FindByNameAsync(RegistrationDetails.UserName);
            if (user is null)
            {
                user = new IdentityUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = RegistrationDetails.UserName
                };

                var result = await _userManager.CreateAsync(user, RegistrationDetails.Password);

                // TODO: Check if it's possible to create role dynamically
                var roleAssignResult = await _userManager.AddToRoleAsync(user, "Consultant");
                if (result.Succeeded && roleAssignResult.Succeeded)
                    return RedirectToPage("../Success");
            }
            else
            {
                ModelState.AddModelError("", "Such user already exists.");
            }

            return Page();
        }
    }
}
