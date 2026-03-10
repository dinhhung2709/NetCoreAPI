using Microsoft.EntityFrameworkCore;
using DemoMvcApp.Models.Entities;

namespace DemoMvcApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}