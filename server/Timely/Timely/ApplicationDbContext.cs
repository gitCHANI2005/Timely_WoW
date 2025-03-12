using Microsoft.EntityFrameworkCore;
using Repository.Entity;

namespace Timely
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // הוספת טבלאות (DbSets)
        public DbSet<User> Users { get; set; }
    }
}
