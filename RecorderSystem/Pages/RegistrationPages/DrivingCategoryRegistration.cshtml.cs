using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecordSystemData.DBContexts;
using RecordSystemLibrary;

namespace RecorderSystem.Pages.RegistrationPages
{
    [Authorize(Policy = "RequireRegistrationRole")]
    public class DrivingCategoryRegistrationModel : PageModel
    {
        private readonly RecordSystemAppContext _recordSystemAppContext;

        [BindProperty]
        public DrivingCategory Category { get; set; }

        public DrivingCategoryRegistrationModel(RecordSystemAppContext recordSystemAppContext)
        {
            _recordSystemAppContext = recordSystemAppContext;
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _recordSystemAppContext.DrivingCategories.AddAsync(Category);
            await _recordSystemAppContext.SaveChangesAsync();
            return RedirectToPage("../Success");
        }
    }
}
