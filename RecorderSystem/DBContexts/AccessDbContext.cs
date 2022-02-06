using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace RecorderSystem
{
    public class AccessDBContext : IdentityDbContext
    {
        private readonly AppSettings _appSettings;
        public AccessDBContext(DbContextOptions<AccessDBContext> options,
            IOptions<AppSettings> settings) : base(options)
        {
            _appSettings = settings.Value ?? throw new ArgumentException(nameof(settings));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_appSettings.RecorderSystemIdentityContext, sql => sql.MigrationsAssembly(typeof(AccessDBContext).GetTypeInfo().Assembly.GetName().Name))
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
        }
    }
}
