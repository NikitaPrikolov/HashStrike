using Microsoft.EntityFrameworkCore;

namespace HashStrike.Api.Models.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Host> Hosts { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
    }
}
