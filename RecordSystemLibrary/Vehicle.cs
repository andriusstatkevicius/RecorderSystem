using System.ComponentModel.DataAnnotations;

namespace RecordSystemLibrary
{
    public class Vehicle
    {
        public int Id { get; set; }
        [Required]
        public string LicencePlate { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public DrivingCategory Category { get; set; }
    }
}
