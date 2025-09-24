*create a new project
dotnet new webapi -n Games

dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

dotnet new tool-manifest
dotnet tool install --local dotnet-ef

dotnet ef dbcontext scaffold "Server=localhost,1433;Database=MyDatabase;User Id=sa;TrustServerCertificate=True;Password=JobMoney12345!" Microsoft.EntityFrameworkCore.SqlServer -o ./Models -c MyDatabaseContext

dotnet ef migrations add <migration_name>
dotnet ef database update