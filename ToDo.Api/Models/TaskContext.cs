using Microsoft.EntityFrameworkCore;

namespace ToDo.API.Models
{
    public class TaskContext : DbContext
    {
        public TaskContext()
        {
        }

        public TaskContext(DbContextOptions<TaskContext> options)
            : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Seed for development
            modelBuilder.Entity<Task>().HasData(
                new Task("Bring The Ring to Rivendell.", true),
                new Task("Defeat Saruman.", true),
                new Task("Destroy The Ring."),
                new Task("Go back to The Shire."),
                new Task("Leave Middle Earth."));
            #endregion
        }
    }
}