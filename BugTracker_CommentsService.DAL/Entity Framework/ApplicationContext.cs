using BugTracker_CommentsService.Domain;
using Microsoft.EntityFrameworkCore;

namespace BugTracker_CommentsService.DAL.Entity_Framework
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().Property(c => c.Content).HasMaxLength(300);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql();
        }
    }
}
