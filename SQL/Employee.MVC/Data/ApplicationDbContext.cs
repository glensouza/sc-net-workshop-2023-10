using Microsoft.EntityFrameworkCore;

namespace Employee.MVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Models.Employee> Employees { get; set; }
    }
}
