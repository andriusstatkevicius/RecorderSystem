using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecorderSystem.AccessEntities;

namespace RecorderSystem.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<IdentityUser> _claimsPrincipalFactory;

        [BindProperty] public LoginInput LoginDetails { get; set; }
        public LoginModel(UserManager<IdentityUser> userManager,
            IUserClaimsPrincipalFactory<IdentityUser> claimsPrincipalFactory)
        {
            _userManager = userManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(LoginDetails.UserName);
                if(!(user is null) && await _userManager.CheckPasswordAsync(user, LoginDetails.Password))
                {
                    // The default implementtion of the claims principal factory is hard coded to use Identity.Application as the cookie scheme
                    var principal = await _claimsPrincipalFactory.CreateAsync(user);
                    await HttpContext.SignInAsync("Identity.Application", principal);
                    return RedirectToPage("./About");
                }

                ModelState.AddModelError("", "Invalid UserName or Password");
            }

            return Page();
        }
    }
}
