using System.ComponentModel.DataAnnotations;

namespace RecordSystemLibrary
{
    public class DrivingCategory
    {
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public List<Student> Student { get; set; } = new List<Student>(); 
    }
}
