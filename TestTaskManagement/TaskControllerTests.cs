using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Controllers;
using TaskManagement.Models;
using TaskManagement.Services;


namespace TestTaskManagement
{
    public class TaskControllerTests
    {
        private readonly Mock<TaskDbContext> _mockContext;
        private readonly Mock<TaskService> _mockTaskService;
        private readonly TaskController _controller;

        public TaskControllerTests()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase(databaseName: "TaskDbTest")
                .Options;

            var context = new TaskDbContext(options);
            _mockContext = new Mock<TaskDbContext>(options);
            _mockTaskService = new Mock<TaskService>(context); // For methods that use service
            _controller = new TaskController(context, _mockTaskService.Object);
        }

        #region CreateTask Tests

        [Fact]
        public async System.Threading.Tasks.Task CreateTask_ValidData_ReturnsCreatedResult()
        {
            // Arrange
            var dto = new TaskCreateDto
            {
                Title = "New Task",
                Description = "Task Description",
                AssignedUserIds = new List<int> { 1, 2 }
            };

            // Act
            var result = await _controller.CreateTask(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var createdTask = Assert.IsType<TaskManagement.Models.Task>(createdResult.Value);
            Assert.Equal(dto.Title, createdTask.Title);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateTask_NullDto_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.CreateTask(null);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Task data must be provided.", badRequest.Value);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateTask_MissingTitle_ReturnsBadRequest()
        {
            // Arrange
            var dto = new TaskCreateDto
            {
                Title = "",
                Description = "Task Description",
                AssignedUserIds = new List<int> { 1 }
            };

            // Act
            var result = await _controller.CreateTask(dto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Task title is required.", badRequest.Value);
        }

        #endregion

        #region GetTaskById Tests

        [Fact]
        public async System.Threading.Tasks.Task GetTaskById_ValidId_ReturnsTask()
        {
            // Arrange
            var task = new TaskManagement.Models.Task
            {
                Title = "Sample Task",
                Description = "Desc",
                Assignments = new List<TaskAssign>()
            };

            var context = new TaskDbContext(
                new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase("GetTaskByIdDb")
                .Options);

            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var controller = new TaskController(context, _mockTaskService.Object);

            // Act
            var result = await controller.GetTaskById(task.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTask = Assert.IsType<TaskManagement.Models.Task>(okResult.Value);
            Assert.Equal(task.Title, returnedTask.Title);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetTaskById_InvalidId_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.GetTaskById(0);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid user ID.", badRequest.Value);
        }

        #endregion

        #region GetTasksByUser Tests

        [Fact]
        public async System.Threading.Tasks.Task GetTasksByUser_ValidUserId_ReturnsTasks()
        {
            // Arrange
            var task = new TaskManagement.Models.Task
            {
                Title = "Sample",
                Assignments = new List<TaskAssign>
                {
                    new TaskAssign { UserId = 1 }
                },
                Description = "Desc"
            };

            var context = new TaskDbContext(
                new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase("GetTasksByUserDb")
                .Options);

            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var controller = new TaskController(context, _mockTaskService.Object);

            // Act
            var result = await controller.GetTasksByUser(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var tasks = Assert.IsAssignableFrom<IEnumerable<object>>(okResult.Value);
            Assert.Single(tasks);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetTasksByUser_NoTasksFound_ReturnsNotFound()
        {
            // Act
            var result = await _controller.GetTasksByUser(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion
    }

}
