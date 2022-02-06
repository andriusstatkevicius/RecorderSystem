using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RecorderSystem;
using RecordSystem.Helpers;
using RecordSystemLibrary;
using System.Reflection;

namespace RecordSystemData.DBContexts
{
    public class RecordSystemAppContext : DbContext
    {
        private readonly AppSettings _appSettings;

        public RecordSystemAppContext(DbContextOptions<RecordSystemAppContext> options,
            IOptions<AppSettings> settings) : base(options)
        {
            _appSettings = settings.Value ?? throw new ArgumentException(nameof(settings));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_appSettings.RecorderSystemAppContext, sql => sql.MigrationsAssembly(typeof(AccessDBContext).GetTypeInfo().Assembly.GetName().Name))
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>().HasIndex(s => new
            {
                s.FirstName,
                s.LastName,
            }).IsUnique();

            builder.Entity<Student>().HasIndex(s => s.EmailAddress).IsUnique();

            builder.Entity<City>().HasIndex(c => c.CityName).IsUnique();
            builder.Entity<Vehicle>().HasIndex(v => v.LicencePlate).IsUnique();
            builder.Entity<DrivingCategory>().HasIndex(v => v.CategoryName).IsUnique();
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<DrivingCategory> DrivingCategories { get; set; }
        public DbSet<ExamRegistration> ExamRegistrations { get; set; }
    }
}