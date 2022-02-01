using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RecorderSystem
{
    public class AccessDBContext : IdentityDbContext
    {
        public AccessDBContext(DbContextOptions<AccessDBContext> options) : base(options) { }
    }
}
