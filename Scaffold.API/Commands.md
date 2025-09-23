dotnet new webapi -m <project>
*validate that the DB is running

*add packages to the project
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

*install tools
dotnet tool install --local dotnet-ef
dotnet new tool-manifest

*Scaffold the database
dotnet tool run dotnet-ef

connection string - "Server=localhost,1433; Database=MyDatabase; User Id=sa;Password=JobMoney12345!"
provider - Microsoft.EntityFrameworkCore.SqlServer

dotnet ef dbcontext scaffold "Server=localhost,1433; Database=MyDatabase;TrustServerCertificate=True; User Id=sa;Password=JobMoney12345!" Microsoft.EntityFrameworkCore.SqlServer -o ./Models -c ChinookContext