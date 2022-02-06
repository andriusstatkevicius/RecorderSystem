using System.ComponentModel.DataAnnotations;

namespace RecordSystemLibrary
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "City or parish name")]
        public string CityName { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
