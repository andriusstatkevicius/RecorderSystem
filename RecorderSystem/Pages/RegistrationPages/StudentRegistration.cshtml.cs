using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecorderSystem.Services;
using RecordSystemData.DBContexts;
using RecordSystemLibrary;

namespace RecorderSystem.Pages.RegistrationPages
{
    [Authorize(Policy = "RequireRegistrationRole")]
    public class StudentRegistrationModel : PageModel
    {
        private readonly RecordSystemAppContext _recordSystemAppContext;
        private readonly IEmailSenderService _emailSenderService;

        [BindProperty]
        public Student Student { get; set; }
        [BindProperty]
        public DateTime Date { get; set; }
        public SelectList CitiesSelection { get; set; }
        public SelectList DrivingCategoriesSelection { get; set; }
        [BindProperty]
        public int DrivingCategoryId { get; set; }
        [BindProperty]
        public int CityId { get; set; }
        [BindProperty]
        public bool IsDrivingExamSelected { get; set; }
        [BindProperty]
        public DateTime DrivingExamTime { get; set; } = DateTime.Now;
        [BindProperty]
        public bool IsTheoryExamSelected { get; set; }
        [BindProperty]
        public DateTime TheoryExamTime { get; set; } = DateTime.Now;
        public StudentRegistrationModel(RecordSystemAppContext recordSystemAppContext,
            IEmailSenderService emailSenderService)
        {
            _recordSystemAppContext = recordSystemAppContext;
            _emailSenderService = emailSenderService;
        }

        public void OnGet() => GetSelectListItems();

        private void GetSelectListItems()
        {
            DrivingCategoriesSelection = new SelectList(_recordSystemAppContext.DrivingCategories, nameof(DrivingCategory.Id), nameof(DrivingCategory.CategoryName));

            CitiesSelection = new SelectList(_recordSystemAppContext.Cities, nameof(City.Id), nameof(City.CityName));
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ByPassValidationForExamDates();

            var drivingCategory = await _recordSystemAppContext.DrivingCategories.SingleAsync(x => x.Id == DrivingCategoryId);
            Student.DrivingCategories.Add(drivingCategory);
            Student.City = await _recordSystemAppContext.Cities.SingleAsync(x => x.Id == CityId);

            if (!ModelState.IsValid)
            {
                GetSelectListItems();
                return Page();
            }

            if (!ValidateInput(out string modelError))
            {
                ModelState.AddModelError("", modelError);
                GetSelectListItems();
                return Page();
            }

            Student.BirthDate = DateOnly.FromDateTime(Date);
            Student.RegistrationDate = DateTime.UtcNow;

            var examRegistrations = IsDrivingExamSelected || IsTheoryExamSelected ? AddExamRegistrations(drivingCategory) : new List<ExamRegistration>();

            await _recordSystemAppContext.Students.AddAsync(Student);
            await _recordSystemAppContext.SaveChangesAsync();

            if (examRegistrations.Any())
                _emailSenderService.SendEmail(Student, examRegistrations);
            return RedirectToPage("../Success");
        }

        private List<ExamRegistration> AddExamRegistrations(DrivingCategory drivingCategory)
        {
            var examRegistrations = new List<ExamRegistration>();
            if (IsTheoryExamSelected)
            {
                var drivingExamRegistration = new ExamRegistration
                {
                    DrivingCategory = drivingCategory,
                    ExamType = ExamType.Theory,
                    Student = Student,
                    TakenAt = TheoryExamTime
                };

                Student.ExamRegistrations.Add(drivingExamRegistration);
                examRegistrations.Add(drivingExamRegistration);
            }

            if (IsDrivingExamSelected)
            {
                var theoryExamRegistration = new ExamRegistration
                {
                    DrivingCategory = drivingCategory,
                    ExamType = ExamType.Practice,
                    Student = Student,
                    TakenAt = DrivingExamTime
                };

                Student.ExamRegistrations.Add(theoryExamRegistration);

                Student.ExamRegistrations.Add(theoryExamRegistration);
                examRegistrations.Add(theoryExamRegistration);
            }

            return examRegistrations;
        }

        private void ByPassValidationForExamDates()
        {
            ModelState[nameof(TheoryExamTime)].ValidationState = ModelValidationState.Valid;
            ModelState[nameof(DrivingExamTime)].ValidationState = ModelValidationState.Valid;
        }

        private bool ValidateInput(out string modelError)
        {
            modelError = string.Empty;

            if (DateTime.Now < Date.AddYears(18))
                modelError = "Student must be at least 18 years old to be registered.\n";

            if (IsDrivingExamSelected && DrivingExamTime < DateTime.Now)
                modelError += "Driving exam date must be in the future.\n";

            if (IsTheoryExamSelected && TheoryExamTime < DateTime.Now)
                modelError += "Theory exam date must be in the future.\n";

            return string.IsNullOrEmpty(modelError);
        }
    }
}
