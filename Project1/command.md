# 🎮 Project Setup Guide

## This document provides step-by-step instructions to set up the Games API project with ASP.NET Core, Entity Framework Core, Swagger, and Serilog.

## Create the Project Structure
dotnet new sln -n Project1
dotnet new webapi -n Games
dotnet sln add ./Games/
dotnet new xunit -n Games.Tests
dotnet sln add ./Games.Tests/

## Add Dependencies
In Games Project:
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Swashbuckle.AspNetCore

## Add Serilog
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Settings.Configuration
dotnet add package Serilog.Sinks.Console

## In Games.Tests Project:
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Xunit

## EF Core
dotnet ef migrations add InitialCreate -o Data/Migrations
dotnet ef database update

## Run the API
dotnet watch run --project Games
Open Swagger UI in your browser: https://localhost:5182/swagger

## Install EF Core Tools Locally
dotnet new tool-manifest
dotnet tool install --local dotnet-ef

## Scaffold DbContext from an Existing Database
dotnet ef dbcontext scaffold "Server=localhost,1433;Database=MyDatabase;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -o ./Models -c MyDatabaseContext

## Create Additional Migrations
dotnet ef migrations add <migration_name>
dotnet ef database update
