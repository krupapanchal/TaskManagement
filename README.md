Sure! Here's a **properly formatted** `README.md` file for your **TaskManagement** project based on what you shared:

---

# TaskManagement

A simple Task Management API built with ASP.NET Core and SQL Server, using JWT authentication and role-based authorization.

## 📋 Prerequisites

- Visual Studio 2022 or later
- SQL Server (any version like SQL Server 2019/2022)
- Postman (optional, for testing APIs)
- Git


## 🛠 Technologies Used

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger for API Documentation


## 🚀 Getting Started

Follow these steps to set up the project locally:

### 1. Clone the Repository

```bash
git clone https://github.com/krupapanchal/TaskManagement.git
```

Navigate into the project folder:

```bash
cd TaskManagement
```

### 2. Import the Database

Copy the .bak file located at DbBackup/TaskMgt.bak from the project folder, and restore it into your SQL Server using SQL Server Management Studio (SSMS).

**Steps to Restore:**
- Open **SQL Server Management Studio (SSMS)**.
- Right-click on **Databases** → **Restore Database**.
- Choose **Device** → Click **Browse**.
- Select the `.bak` file you downloaded.
- Follow the prompts to complete the restore.

👉 Need help? Check this quick guide: https://sqlbak.com/blog/complete-guide-to-restoring-sql-databases-from-bak-files-in-sql-server/

### 3. Update the Database Connection String

- Open the `appsettings.json` file in the project.
- Find the `ConnectionStrings` section.
- Update it according to your SQL Server setup:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=YOUR_DATABASE_NAME;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> Replace `YOUR_SERVER_NAME` and `YOUR_DATABASE_NAME` with your local server and database names.

### 4. Run the Project

- Open the project in Visual Studio.
- Build the solution.
- Run the project.

✅ On first run, **seed data** (default users/roles) will be automatically inserted into the database.

---

## 📖 Using Swagger UI

When the project runs, Swagger UI will automatically open at:

```
https://localhost:{port}/swagger
```

Here you can explore and test all the API endpoints.

---

## 🔐 Authentication and Authorization

Before using protected APIs, you must log in and authorize:

1. **Login:**
   - Use the `Login` endpoint in Swagger.
   - Provide valid credentials (seeded users).

2. **Get JWT Token:**
   - After successful login, a **JWT token** will be returned.

3. **Authorize Requests:**
   - Click the **Authorize** button in Swagger (top-right).
   - Paste your token **with** the `Bearer` prefix.

Example:

```
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

After authorization, you can access protected APIs like:

- `CreateTask`
- `GetTaskById`
- `GetTaskByUser`

---

## 👥 Seeded Users

| Role  | Username | Password |
|:-----:|:--------:|:--------:|
| Admin | admin    | admin123|
| User  | user     | user123 |

> You can modify the seed data later based on your project needs.



📚 Task 2: Database Design Basics
This document contains the database deliverables for the Task Management system.

	📦 Deliverables
		1. SQL or EF Core Migration Scripts
		Ans: Migration scripts are included to create tables like:
				Users
				Tasks
				TaskAssign
				TaskComments
			Seeder is configured to automatically insert sample data (default users, roles, etc.) during the first run.
			No manual data entry required after database setup.

		2. ER Diagram
		Ans: A detailed ER diagram for the database design has been created.
			You can view it here:
			🔗 https://drive.google.com/file/d/1cGDKZhb0NOMniukpVablZo2UjLMk20QR/view

			The diagram is created using dbdiagram.io and outlines the relationships between Users, Tasks, Task Assignments, and Task Comments.

		3. Sample SQL Queries
		Ans: Below are sample queries for common operations:

			🔎 Get All Tasks Assigned to a Specific User

			SELECT * 
			FROM Tasks t
			JOIN TaskAssign ta ON t.Id = ta.TaskId
			WHERE ta.UserId = 2;

			🔎 Get All Comments for a Specific Task

			SELECT * 
			FROM TaskComments
			WHERE TaskId = 3;

			🗂️ Tables Overview

			Table Name	Description
			Users	Stores user information (Admin/User)
			Tasks	Stores tasks created in the system
			TaskAssign	Maps tasks to assigned users
			TaskComments	Stores comments on tasks

			📖 Notes
			Seeder: On the first application run, it inserts default admin and user accounts, example tasks, and example assignments.

			Foreign Keys: Relationships are properly enforced between tables to maintain data integrity.



🐞 Task 3: Debugging & Code Fixing
	This document outlines the code fixes made to the Task Management project.

	📦 Deliverables
		1. Fixed Code File
		Ans: The fixed code files are already added to the project repository.

			All identified issues have been resolved to ensure proper asynchronous behavior and improved error handling.

		2. Short Explanation of Changes
		Ans: Here’s a summary of the key fixes and improvements:

			✅ Return Types	: Changed List<Task> to Task<List<Task>> and Task<Task> to Task<Models.Task> for async correctness.
			✅ Await Usage : Added await before all async database calls to properly handle asynchronous execution.
			✅ Method Naming : Renamed methods to add the Async suffix (e.g., GetTaskAsync, GetAllTasksAsync) following C# best practices.
			✅ Error Handling: Wrapped database operations inside try-catch blocks to handle and throw detailed error messages.
			
			🧹 Summary of Improvements
			Better Async Handling: Prevents blocking and improves application scalability.
			Clean Code Standards: Improved method naming makes the code more readable and maintainable.
			Robustness: Enhanced error management ensures easier debugging and more informative exceptions.


📋Task 5. Unit Testing
	Write unit tests to validate the core functionalities of the Task Management API, mainly focusing on:

	Controller Methods (TaskController)

	📦 Deliverables:
	1. Test Project Folder Structure

		/TaskManagement.Tests
		│
		├── Controllers
		│   └── TaskControllerTests.cs     // Unit tests for TaskController
		│
		├── Services
		│   └── TaskServiceTests.cs        // (Optional) Unit tests for TaskService
		│
		├── Mocks
		│   └── MockDbContext.cs           // (Optional) Helpers for mocking DbContext
		│
		├── TaskManagement.Tests.csproj    // Test project file (xUnit, Moq installed)
		│
		└── README.md                      // Instructions for running tests
		
	2. README.md (or comment inside the code)

	# 🧪 TaskManagement.Tests

	This project contains **unit tests** for the `TaskManagement` API, focusing on validating important controller and service methods.

	---

	## 🚀 How to Run the Tests

	1. **Open the Solution**
	   - Open `TaskManagement.sln` in **Visual Studio 2022+** 

	2. **Install Required Packages** (if missing)
	   - `xunit`
	   - `moq`
	   - `Microsoft.EntityFrameworkCore.InMemory`
	   - `xunit.runner.visualstudio`

	3. **Build the Solution**
	   - Press `Ctrl + Shift + B` or right-click ➔ **Build**.

	4. **Run Tests**
	   - Open **Test Explorer** (Visual Studio: Test > Test Explorer).
	   - Click **Run All Tests** or run individual tests.

	🛠 Technologies Used

		Tool
		xUnit	
			Testing framework
		Moq	
			Mocking services/dependencies
		EF Core InMemory	
			Simulated database for testing
			
	⚠️ Important Notes
		Always mock services (e.g., ITaskService) rather than actual DbContext directly.
		Use InMemory database to avoid hitting real database.
		Cover both positive and negative scenarios (valid inputs, invalid inputs, error handling).


## 📽 Video Walkthrough

 https://www.loom.com/share/65371e3bc5aa4cd9ae8a59f5a360b16e?sid=363c78d1-7f0f-4da9-a64f-8196887c10d5



