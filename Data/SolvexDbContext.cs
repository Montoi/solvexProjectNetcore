using Microsoft.EntityFrameworkCore;
using solvexProject.Models;

namespace solvexProject.Data
{
    public class SolvexDbContext : DbContext
    {
        public SolvexDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

    }
}
