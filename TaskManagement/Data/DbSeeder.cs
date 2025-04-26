using TaskManagement.Models;
using Task = TaskManagement.Models.Task;

namespace TaskManagement.Data
{
    public static class DbSeeder
    {
        public static void Seed(TaskDbContext context)
        {
            if (!context.Users.Any())
            {
                var users = new List<Users>
                {
                    new Users { Username = "admin", Password = "admin123", Role = Role.Admin },
                    new Users { Username = "john", Password = "john123", Role = Role.User },
                    new Users { Username = "jane", Password = "jane123", Role = Role.User }
                };
                context.Users.AddRange(users);
                context.SaveChanges();
            }

            if (!context.Tasks.Any())
            {
                var tasks = new List<Task>
            {
                new Task { Title = "Fix login bug", Description = "Resolve the null reference error" },
                new Task { Title = "Write documentation", Description = "Complete API docs" },
                new Task { Title = "Deploy app", Description = "Deploy to staging environment" }
            };
                context.Tasks.AddRange(tasks);
                context.SaveChanges();
            }

            if (!context.TaskAssign.Any())
            {
                var assigns = new List<TaskAssign>
            {
                new TaskAssign { TaskId = 1, UserId = 2 },
                new TaskAssign { TaskId = 2, UserId = 3 },
                new TaskAssign { TaskId = 3, UserId = 2 }
            };
                context.TaskAssign.AddRange(assigns);
                context.SaveChanges();
            }

            if (!context.TaskComments.Any())
            {
                var comments = new List<TaskComment>
            {
                new TaskComment { TaskId = 1, UserId = 2, Comment = "I am on it." },
                new TaskComment { TaskId = 2, UserId = 3, Comment = "Docs are half done." },
                new TaskComment { TaskId = 3, UserId = 2, Comment = "Deployment completed." }
            };
                context.TaskComments.AddRange(comments);
                context.SaveChanges();
            }
        }
    }
}
    