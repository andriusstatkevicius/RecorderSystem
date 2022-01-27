using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecorderSystem.Entities;

namespace RecorderSystem.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public RegistrationInput RegistrationDetails { get; set; }
        public RegisterModel(UserManager<IdentityUser> userManager)
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
                if (result.Succeeded)
                    return RedirectToPage("./Success");
            }

            return Page();
        }
    }
}
