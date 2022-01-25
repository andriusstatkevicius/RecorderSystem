using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecorderSystem.Services;

namespace RecorderSystem.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ISessionContextProvider _sessionContextProvider;

        public LoginModel(ISessionContextProvider sessionContextProvider)
        {
            _sessionContextProvider = sessionContextProvider;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _sessionContextProvider.IsLoggedIn = true;
            return new OkResult();
        }
    }
}
