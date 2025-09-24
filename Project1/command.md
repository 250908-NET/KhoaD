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

*in Project1.Tests
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Xunit

dotnet new tool-manifest
dotnet tool install --local dotnet-ef

dotnet ef dbcontext scaffold "Server=localhost,1433;Database=MyDatabase;User Id=sa;TrustServerCertificate=True;Password=<password>" Microsoft.EntityFrameworkCore.SqlServer -o ./Models -c MyDatabaseContext

dotnet ef migrations add <migration_name>
dotnet ef database update