# Application Tracker C# Web API

This is a simple solution which allows user to view, create and update applications.

The solution seeds three dummy data for viewing purposes.

API documentation can be viewed in http://localhost:5262/swagger/index.html

## Instructions to run the solution:
1. In the terminal, make sure to navigate to "\ApplicationTracker\ApplicationTracker"
2. For the database, run the command: 'dotnet ef migrations add Initial'
3. Then, run the command: 'dotnet ef database update' 
4. When the database has been created, run the command: 'dotnet run' or click on the Run 'ApplicationTracker:http' button

## Assumptions/Limitations
1. The Visual Studio has EF Core nuget packages installed
2. No account is used for creating and updating the applications
3. No sign in is required to access the application