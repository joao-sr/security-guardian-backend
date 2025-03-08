using Guardian.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Persistence.DatabaseContext
{
    public class GuardianDatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public GuardianDatabaseContext(DbContextOptions<GuardianDatabaseContext> options): base(options)
        {
            
        }

        // add the entities
        // public DbSet<entityName> entityName(s) {get; set;}

        // let the dbcontext know about configuration files
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(GuardianDatabaseContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
