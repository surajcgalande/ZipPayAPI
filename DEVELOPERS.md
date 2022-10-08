# User API Dev Guide
This API is developed using .net core 3.1 Web API. 
The following custom middlewares are implemented:
1. UseCorsPolicy - To accept the requests from specific hosts.
2. ConfigureExceptionMiddleware - To handle exceptions in this API and to log exception details to database.

Database:
Database connection string is stored in appsettings.json file. Please update connection string to your databse server before running application.
SQL Server is used as a Database.

## Building
To build the project, you can use build option or rebuild option in VS or appropriate commands for CLI.

## Testing
Please use SwaggerUI for testing API by browsing to /swagger endpoint.
NUnit test cases are written for testing UsersController and AccountController.
Open TestExplorer to run the tests.

## Deploying
For deploying this API, you can use publish option to deploy package to file location or any FTP server.

## Additional Information
Business Logic is implemented as specified in ReadMe.md file.