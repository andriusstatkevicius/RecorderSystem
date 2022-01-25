using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecorderSystem.Services;

namespace RecorderSystem.Pages
{
    public class AdminModel : PageModel
    {
        private readonly ILogger<AdminModel> _logger;
        private readonly ISessionContextProvider _sessionContextProvider;
        public bool IsLoggedIn => _sessionContextProvider?.IsLoggedIn == true;

        public AdminModel(ILogger<AdminModel> logger, 
            ISessionContextProvider sessionContextProvider)
        {
            _logger = logger;
            _sessionContextProvider = sessionContextProvider;
        }

        public void OnGet()
        {
            if (_sessionContextProvider.IsLoggedIn)
            {
                Console.WriteLine("Logged in");
            }
        }
    }
}