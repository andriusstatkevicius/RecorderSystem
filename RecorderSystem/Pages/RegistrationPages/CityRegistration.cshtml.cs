using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecordSystemData.DBContexts;
using RecordSystemLibrary;

namespace RecorderSystem.Pages.RegistrationPages
{
    [Authorize(Policy = "RequireRegistrationRole")]
    public class CityRegistrationModel : PageModel
    {
        private readonly RecordSystemAppContext _recordSystemAppContext;

        [BindProperty]
        public City City { get; set; }

        public CityRegistrationModel(RecordSystemAppContext recordSystemAppContext)
        {
            _recordSystemAppContext = recordSystemAppContext;
        }
        
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            if (await _recordSystemAppContext.Cities.AnyAsync(c => c.CityName == City.CityName && c.Country == City.Country))
            {
                ModelState.AddModelError("", "City with the current details already exists");
                return Page();
            }

            await _recordSystemAppContext.Cities.AddAsync(City);
            await _recordSystemAppContext.SaveChangesAsync();
            return RedirectToPage("../Success");
        }
    }
}
