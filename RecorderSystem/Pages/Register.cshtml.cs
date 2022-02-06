using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecorderSystem.AccessEntities;

namespace RecorderSystem.Pages
{
    [Authorize(Policy = "RequireRegistrationRole")]
    public class RegisterModel : PageModel
    {
        private readonly IHtmlHelper _htmlHelper;

        [BindProperty]
        public UserType UserType { get; set; }
        public IEnumerable<SelectListItem> UserTypes { get; set; }
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterModel(IHtmlHelper htmlHelper, UserManager<IdentityUser> userManager)
        {
            _htmlHelper = htmlHelper;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (await _userManager.IsInRoleAsync(user, "administrator"))
                UserTypes = _htmlHelper.GetEnumSelectList<UserType>();
            else // Not allowing for consultant to enter new users except students (only the admin can add new employees)
                UserTypes = _htmlHelper.GetEnumSelectList<UserType>()
                .Where(x => x.Text.Equals(nameof(UserType.Student)));
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (UserType == UserType.Student)
                return RedirectToPage("./RegistrationPages/StudentRegistration");
            else
                return RedirectToPage("./RegistrationPages/ConsultantRegistration");

            return Page();
        }
    }
}
