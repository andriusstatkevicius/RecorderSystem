using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecorderSystem.Pages
{
    public class AdminModel : PageModel
    {
        private readonly ILogger<AdminModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminModel(ILogger<AdminModel> logger,
            UserManager<IdentityUser> userManager
            )
        {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}