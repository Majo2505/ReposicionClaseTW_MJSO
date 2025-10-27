using Microsoft.EntityFrameworkCore;
using EC4clase1.Models;
namespace EC4clase1.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Hospital> Hospitals => Set<Hospital>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<Hospital>()
                .HasIndex(h => h.Name) // quiero q se indexe por sus nombres
                .IsUnique(); // solo puede haber un hospital con ese nombre
        }

    }
}
