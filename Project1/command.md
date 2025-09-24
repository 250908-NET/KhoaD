*create a new project (Under Project1)
dotnet new sln
dotnet new webapi -n Games
dotnet sln add ./Games/
dotnet new xunit -n Games.Tests

*in Games folder
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Swashbuckle.AspNetCore   # Swagger UI

*serilog
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Settings.Configuration
dotnet add package Serilog.Sinks.Console

*in Project1.Tests
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Xunit

*Manage Schema
dotnet ef migrations add InitialCreate -o Data/Migrations
dotnet ef database update

*run
dotnet watch run --project

*website
https://localhost:5182/swagger






dotnet new tool-manifest
dotnet tool install --local dotnet-ef

dotnet ef dbcontext scaffold "Server=localhost,1433;Database=MyDatabase;User Id=sa;TrustServerCertificate=True;Password=<password>" Microsoft.EntityFrameworkCore.SqlServer -o ./Models -c MyDatabaseContext

dotnet ef migrations add <migration_name>
dotnet ef database update