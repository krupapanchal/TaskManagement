namespace TaskManagement.Models
{
    /// <summary>
    /// Represents a data transfer object for creating a new task.
    /// </summary>
    public class TaskCreateDto
    {
        // Gets or sets the title of the task.
        public string Title { get; set; }

        // Gets or sets the description of the task.
        public string Description { get; set; }

        // Gets or sets the list of user IDs assigned to the task.
        public List<int> AssignedUserIds { get; set; }
    }
}
