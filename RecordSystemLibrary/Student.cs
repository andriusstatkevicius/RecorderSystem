using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecordSystemLibrary
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Birth Date")]
        public DateOnly BirthDate { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }
        [Display(Name = "Mobile phone")]
        public string MobilePhone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Address { get; set; }
        public City City { get; set; }
        public List<DrivingCategory> DrivingCategories { get; set; } = new List<DrivingCategory>();
        public List<ExamRegistration> ExamRegistrations { get; set; } = new List<ExamRegistration>();
    }
}
