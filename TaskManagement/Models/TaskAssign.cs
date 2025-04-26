namespace TaskManagement.Models
{
    /// <summary>
    /// Represents a task assignment entity.
    /// </summary>
    public class TaskAssign
    {
        /// <summary>
        /// Gets or sets the unique identifier for the task assignment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key referencing the Task entity.
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Gets or sets the foreign key referencing the User entity.
        /// </summary>
        public int UserId { get; set; }
    }
}
