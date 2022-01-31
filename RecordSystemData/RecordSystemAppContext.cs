using Microsoft.EntityFrameworkCore;
using RecordSystemLibrary;

namespace RecordSystemData
{
    public class RecordSystemAppContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<DrivingCategory> MyProperty { get; set; }
    }
}