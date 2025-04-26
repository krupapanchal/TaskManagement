using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;
using Task = TaskManagement.Models.Task;

namespace TaskManagement.Services
{
    /// <summary>
    /// Creating for Bugfix regarding Task
    /// </summary>
    public class TaskService
    {  
        private readonly TaskDbContext _dbContext;
        public TaskService(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Task> GetTaskAsync(int id)
        {
            try
            {
                return await _dbContext.Tasks.Include(t=>t.Assignments).FirstOrDefaultAsync(t => t.Id == id);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error fetching tasks", ex); 
            }
        }
        public async Task<List<Task>> GetAllTasksAsync()
        {
            try
            {
                return await _dbContext.Tasks.Include(t => t.Assignments).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error fetching tasks", ex);
            }
           
        }
    }
}
