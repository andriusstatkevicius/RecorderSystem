using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecorderSystem.AccessEntities;

namespace RecorderSystem.Pages.RegistrationPages
{
    [Authorize(Roles = "Administrator")]
    public class ConsultantRegistrationModel : PageModel
    {
        private const string CONSULTANT_ROLE_NAME = "Consultant";
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        [BindProperty]
        public EmployeeRegistrationInput RegistrationDetails { get; set; }
        
        public ConsultantRegistrationModel(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", string.Join(Environment.NewLine, result.Errors.Select(x => x.Description)));
                    return Page();
                }

                if (!await _roleManager.RoleExistsAsync(CONSULTANT_ROLE_NAME))
                    await _roleManager.CreateAsync(new IdentityRole(CONSULTANT_ROLE_NAME));

                var roleAssignResult = await _userManager.AddToRoleAsync(user, CONSULTANT_ROLE_NAME);
                if (roleAssignResult.Succeeded)
                {
                    return RedirectToPage("../Success");
                }
                else
                {
                    ModelState.AddModelError("", string.Join(Environment.NewLine, roleAssignResult.Errors.Select(x => x.Description)));
                    return Page();
                }
            }
            else
            {
                ModelState.AddModelError("", "Such user already exists.");
            }

            return Page();
        }
    }
}
