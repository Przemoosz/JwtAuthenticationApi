# Changelog

Changelog for JwtAuthenticationApi System. 

## [1.0.0] - 03.01.2024 - JAA-5-ImplementConnectionToPostgreSQL

### Added
- NuGet package: Npgsql.EntityFrameworkCore.PostgreSQL version 7.0.11

### Changed
- Changed supported database to PostgreSQL
- Updated EntityFramework to version 7.0.14 

### Removed
- Nuget package: Microsoft.EntityFrameworkCore.SqlServer

## [0.4.0] - 04.08.2023 - JAA-4-ImplementRegisterEndpoint 

### Added
- Register user controller with register user endpoint.
- Password validation based on rule engine.
- Rules for password validation.
- SaltService method to create salt.
- New command to create user entity from user register request.
- ListExtension.
- User service responsible for saving and getting user to and from database.
- Error types enum.
- Missing XML documentation.
- User register services that handels registeration logic.
- Password validation Exception.

### Changed
- SaltService CreateAndSave method are now separeted.
- Test controller is now marked as obsolete
- Models id is no longer GUID (Changed to int)
- Models are no longer saved in database (changed to entity)
- Username is now unique value


### Removed
- Microsoft.Extensions.Logging.ILogger is no longer basic logging system.

## [0.3.0] - 07.05.2023 - JAA-3-ImplementLoggingUsingSerilog

### Added
- NuGet packages: Serilog, Polly, Serilog.AspNetCore, Serilog.Enrichers.Thread, Serilog.Sinks.Async, Serilog.Sinks.Console, 
Serilog.Sinks.File, SerilogWeb.Classic
- LoggerSetup class that setup ILogger from Serilog.
- Logging in SaltService.
- Retry policy in SaltService using Polly.
- Tests for all new classes.
- New tests for polly in SaltService.
- PollySleepingIntervalsFactory class that creates sleeping intervals for Polly.
- Semaphore wrapper and semaphore wrapper factory.
- RegisterServices method in ContainerSetup.
- SettingsNotProvided Exception.
- ReturnHttpNotAcceptable option in setup.
- Try-Catch statements in SaltService. 

### Changed
- Lock in SaltService is now done using Semaphore.
- All tests received "Pararelizable" attribute.
- Tests in SaltService.
- Switched to Widnows VM in github actions to gain ability to use semaphore.

### Removed
- Microsoft.Extensions.Logging.ILogger is no longer basic logging system.

## [0.2.0] - 05.03.2023 - JAA-2-ImplementPasswordHashing

### Added
- Command pattern with ICommand<T> and CommandHandler.
- Command handling exception.
- Commands factory class.
- Result model that contains information about operation status and result value.
- Password pepper provided as an application secret.
- Password mixing command that combines password, salt and pepper.
- Password hashing service, that hashes password using SHA256.
- SaltProvider class, that provides password salts and decides if new salt should be created.
- SaltService that handles database operations with password salt.
- Unit tests for all new classes.
- Test controller for testing purpose (will be deleted in future).
- Wrapper classes for Guid and mutex.
- Factory class for mutex wrapper.
- New XML documentation for all classes.

### Changed
- Container is now set up using extensions methods.
- Updated Readme.md.

### Removed
- WeatherForecastController with its endpoints.

## [0.1.0] - 18.03.2023 - JAA-1-SetUpDatabaseWithUserAndSaltTables

### Added
- EntityFramework Nuget package with related packages (EntityFrameworkCore.Design and EntityFrameworkCore.SqlServer).
- User identity and password salt model.
- User identity and password salt databases contexts.
- Registered databases contexts in application.
- Added Local installation tutorial in Readme.md.

### Changed
- Updated Readme.md to include new nuget packages.
- WeatherForecastController is temporarily used as a test controller.

### Removed
- WeatherForecast class

## [0.0.0] - 16.03.2023 - JAA-0-SetUpSolutionAndProjects

### Added
- JwtAuthenticationApi project as ASP.NET Core Web API based on .NET 6.
- Unit test project based on NUnit test framework.
- Nuget packages for test solution.
- Database connection string that are provided using IOptions pattern.