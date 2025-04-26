# TaskManagement

1. Clone the Repository

git clone https://github.com/krupapanchal/TaskManagement.git
Navigate into the project folder:


2. Import the Database
Download and restore the .bak file by following this guide:
How to Restore .bak File in SQL Server

(In short: Open SSMS → Right-click on "Databases" → "Restore Database" → Choose Device → Browse and select the .bak file.)

3. Update the Database Connection String
Open the appsettings.json file.

Find the ConnectionStrings section.

Update it to match your local SQL Server setup.
Example:

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=YOUR_DATABASE_NAME;Trusted_Connection=True;TrustServerCertificate=True;"
}
4. Run the Project
Build and run the project from Visual Studio.

When the project runs for the first time, the required seed data will automatically be inserted into the database.

5. Using Swagger UI
Once the application starts, Swagger UI will open automatically (URL will be something like https://localhost:xxxx/swagger).

6. Authenticate and Authorize
First, call the Login API available on Swagger.

It will return a JWT token.

Copy the token.

Click the "Authorize" button in Swagger (top-right).

Paste the token into the textbox (including the word Bearer prefix if needed).

Example:

Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
After authorization, you will be able to access the protected APIs:

CreateTask

GetTaskById

GetTaskByUser

