namespace TaskManagement.Models
{
    public class TaskComment
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public Task Task { get; set; }
        public Users User { get; set; }
    }
}
