using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecordSystemData.DBContexts;
using RecordSystemLibrary;

namespace RecorderSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RecordSystemAppContext _recordSystemAppContext;

        [BindProperty(SupportsGet = true)]
        public string SearchedId { get; set; }
        public ExamRegistration Registration { get; set; }


        public IndexModel(RecordSystemAppContext recordSystemAppContext)
        {
            _recordSystemAppContext = recordSystemAppContext;
        }

        public async Task OnGetAsync()
        {
            var examRegistration = await _recordSystemAppContext.ExamRegistrations.Include(x => x.Student)
                .Include(x => x.DrivingCategory)
                .FirstOrDefaultAsync(x => x.Id == SearchedId);
            
            if (!(examRegistration is null))
                Registration = examRegistration;
        }
    }
}