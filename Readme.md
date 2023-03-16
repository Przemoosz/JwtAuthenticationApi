# JWT Authentication API System

## Description
Jwt authentication api system is a .NET Core Web API. System provides endpoints for creating user and logging user with to receive Json Web Token (JWT) that could be used in external applications. Generated token will have claims that You can use in your app to authorize user. API also will prvide endpoints for administrator.

## Background
Application is based on ASP.NET Core Web Api 6.0 (.NET 6) and uses Azure SQL as database. Unit test project is also based on .NET 6. C# language verion is 10.

## Branches
- JAA-0-SetUpSolutionAndProjects - Set up JWT Authentication API project and JWT Authentication API unit tests project.
- JAA-1-SetUpDatabaseWithUserAndSaltTables - Set up model for user and user password salt.
- JAA-2-ImplementPasswordHashing - Implement password hashing based on salt and pepper.
- JAA-3-ImplementVerifyHashedPassword - Implement password veryfying.
- JAA-4-ImplementRegisterEndpoint - Implement register endpoint
- JAA-5-ImplementJWTFactory - Implement class that will create JWT based on secret and claims.
- JAA-6-ImplementLogInEndpoint - Implement log in endpoint that returns JWT.
- JAA-7-ImplementCreateAdminUserEndpoint - Implement create admin user endpoint that will authorize if provided JWT is correct.

## Architecture
TODO

## Instalation
TODO

## Nugets
- Any - ver. 9.2.0
- coverlet.collector - ver. 3.2.0
- FluentAssertions - ver. 6.10.0
- Microsoft.NET.Test.Sdk - ver. 17.5.0
- NSubstitute - ver. 5.0.0
- NUnit - ver. 3.13.3
- NUnit.Analyzers - ver. 3.6.1
- NUnit3TestAdapter - ver. 4.4.2
- Swashbuckle.AspNetCore - ver. 6.5.0
- TddXt.Any.Extensibility - ver. 6.7.0


## Configuration
To run your app corectly you have to provide correct values in appsettings.json file in JwtAuthentiationApi file. Json file should looks like this:
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
    }
}
```