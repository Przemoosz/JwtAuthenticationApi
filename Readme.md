# JWT Authentication API System

## Description
Jwt authentication api system is a .NET Core Web API. System provides endpoints for creating user and logging user with to receive Json Web Token (JWT) that could be used in external applications. 
Generated token will have claims that You can use in your app to authorize user. 
API also will provide endpoints for administrator.

## Background
Application is based on ASP.NET Core Web Api 6.0 (.NET 6) and uses Azure SQL as database. Unit test project is also based on .NET 6.
C# language version is 10.

## Branches
- JAA-0-SetUpSolutionAndProjects - Set up JWT Authentication API project and JWT Authentication API unit tests project.
- JAA-1-SetUpDatabaseWithUserAndSaltTables - Set up model for user and user password salt.
- JAA-2-ImplementPasswordHashing - Implement password hashing based on salt and pepper.
- JAA-3-ImplementLoggingUsingSerilog - Implement logging using serilog.
- JAA-3-ImplementVerifyHashedPassword - Implement password verifying.
- JAA-4-ImplementRegisterEndpoint - Implement register endpoint
- JAA-5-ImplementJWTFactory - Implement class that will create JWT based on secret and claims.
- JAA-6-ImplementLogInEndpoint - Implement log in endpoint that returns JWT.
- JAA-7-ImplementCreateAdminUserEndpoint - Implement create admin user endpoint that will authorize if provided JWT is correct.

## Architecture
TODO

## Local Installation

1. Clone repository from main branch.
2. Create appsettings.json file and provide connection strings (Look - Configuration).
3. Go to JwtAuthenticationApi project file, and open powershell.
    
    A.  Type
    ``` 
     dotnet ef migrations add InitialMigrationForUser --context UserContext
    ```
    This will create initial migration for user identity database.

    B.  Type 
    ``` dotnet
     dotnet ef migrations add InitialMigrationForPasswordSalt --context PasswordSaltContext
    ```
    This will create initial migration for password salt database.

    C. Type:
    ```
    dotnet ef database update --context UserContext
    ```
    This command will update your user identity database.

    D. Type:
    ```
    dotnet ef database update --context PasswordSaltContext
    ```
    This command will update your password salt database.

4. In this same location type:
```
dotnet run
```
To run application locally. After this you should see in console that application is running. Hit CTRL+C to stop application.

## Nuget Packages
- Any - ver. 9.2.0
- coverlet.collector - ver. 3.2.0
- EmailValidation - ver. 1.0.8
- FluentAssertions - ver. 6.10.0
- Microsoft.EntityFrameworkCore - ver. 7.0.4
- Microsoft.EntityFrameworkCore.Design - ver. 7.0.4
- Microsoft.EntityFrameworkCore.SqlServer - ver. 7.0.4
- Microsoft.NET.Test.Sdk - ver. 17.5.0
- NSubstitute - ver. 5.0.0
- NUnit - ver. 3.13.3
- NUnit.Analyzers - ver. 3.6.1
- NUnit3TestAdapter - ver. 4.4.2
- Polly - ver. 7.2.3
- Serilog - ver 2.12.0
- Serilog.AspNetCore - ver. 6.1.0
- Serilog.Enrichers.Thread ver. 3.1.0
- Serilog.Sinks.Async ver. 1.5.0
- Serilog.Sinks.Console ver. 4.1.0
- Serilog.Sinks.File ver. 5.0.0
- SerilogWeb.Classic ver. 5.1.66
- SerilogWeb.Classic.WebApi ver. 4.0.5
- Swashbuckle.AspNetCore - ver. 6.5.0
- TddXt.Any.Extensibility - ver. 6.7.0


## Configuration
To run your app correctly you have to provide correct values in appsettings.json file in JwtAuthenticationApi file. 
Json file should look like this:
```json
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*",
    "DatabaseConnectionStrings": {
        "IdentityDatabaseConnectionString": "Connection string to identity database",
        "SaltDatabaseConnectionString": "Connection string to database that contains salt"
    },
    "PasswordPepper": {
        "Pepper": "Your secret password pepper, It is better to have it as long as possible"
    }
}
```