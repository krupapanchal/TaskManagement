using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Models
{
    /// <summary>
    /// Represents the database context for the task management application.
    /// </summary>
    public class TaskDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for the database context.</param>
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }


        // Gets or sets the database set for users.
        public DbSet<Users> Users { get; set; }

        // Gets or sets the database set for tasks.
        public DbSet<Task> Tasks { get; set; }

        // Gets or sets the database set for task comments.
        public DbSet<TaskComment> TaskComments { get; set; }

        // Gets or sets the database set for task assignments.
        public DbSet<TaskAssign> TaskAssign { get; set; }
    }
}
