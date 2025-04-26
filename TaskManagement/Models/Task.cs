namespace TaskManagement.Models
{
    /// <summary>
    /// Represents a task entity.
    /// </summary>
    public class Task
    {
        // Initializes a new instance of the <see cref="Task"/> class.
        public Task()
        {
            // Initialize the assignments collection to an empty list
            Assignments = new List<TaskAssign>();
        }

        // Gets or sets the unique identifier for the task.
        public int Id { get; set; }

        // Gets or sets the title of the task.
        public string Title { get; set; }

        // Gets or sets the description of the task.
        public string Description { get; set; }

        // Gets or sets the collection of task assignments.
        public virtual ICollection<TaskAssign> Assignments { get; set; }
    }
}
