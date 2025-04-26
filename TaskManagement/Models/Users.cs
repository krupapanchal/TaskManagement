namespace TaskManagement.Models
{

    public enum Role
    {
        Admin,
        User
    }
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; } // Admin or User
    }
}
