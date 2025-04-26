using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagement.Models;
using TaskManagement.Services;
using Task = TaskManagement.Models.Task;

namespace TaskManagement.Controllers
{
    /// <summary>
    /// Controller for managing tasks.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly TaskDbContext _context;
        private readonly TaskService _taskService;
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskController"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public TaskController(TaskDbContext context, TaskService taskService)
        {
            _context = context;
            _taskService = taskService;
        }


        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="dto">The task creation data transfer object.</param>
        /// <returns>A created action result with the newly created task.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto dto)
        {

            if (dto == null)
                return BadRequest("Task data must be provided.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest("Task title is required.");
            try
            {
                var task = new Task
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    Assignments = dto.AssignedUserIds.Select(userId => new TaskAssign
                    {
                        UserId = userId,
                    }).ToList()
                };

                // Add the task to the database context
                _context.Tasks.Add(task);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Return a created action result with the newly created task
                return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An error occurred while creating the task: {ex.Message}");
            }
            // Create a new task from the provided DTO
           
            
        }

        /// <summary>
        /// Gets a task by its ID.
        /// </summary>
        /// <param name="id">The ID of the task to retrieve.</param>
        /// <returns>A not found result if the task is not found, otherwise an OK result with the task.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {

            if (id <= 0)
                return BadRequest("Invalid user ID.");

            // Retrieve the task from the database, including its assignments
            var task = await _context.Tasks.Include(t => t.Assignments).FirstOrDefaultAsync(t => t.Id == id);

            // If the task is not found, return a not found result
            if (task == null)
                return NotFound();

            // Return an OK result with the task
            return Ok(task);
        }

        /// <summary>
        /// Gets tasks assigned to a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve tasks for.</param>
        /// <returns>A not found result if no tasks are found, otherwise an OK result with the tasks.</returns>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTasksByUser(int userId)
        {

            if (userId <= 0)
                return BadRequest("Invalid user ID.");

            // Retrieve tasks from the database that are assigned to the specified user
            var tasks = await _context.Tasks
                .Where(t => t.Assignments.Any(a => a.UserId == userId))
                .Select(t => new
                 {
                     t.Id,
                     t.Title,
                     t.Description,
                     Assignments = t.Assignments
                         .Where(a => a.UserId == userId)
                         .Select(a => new
                         {
                             a.Id,
                             a.UserId,
                             a.TaskId
                         })
                         .ToList()
                 })
                 .ToListAsync();

            // If no tasks are found, return a not found result
            if (!tasks.Any())
                return NotFound();

            // Return an OK result with the tasks
            return Ok(tasks);
        }

        //Task 3

        [HttpGet("GetTask/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {

                var task = await _taskService.GetTaskAsync(id);
                if (task == null)
                    return NotFound();
                return Ok(task);
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }


        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }
    }
}
