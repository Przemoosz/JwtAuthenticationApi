# Changelog

Changelog for JwtAuthenticationApi System. 

## [0.2.0] - xx.03.2023 - JAA-2-ImplementPasswordHashing

### Added
- Command pattern with ICommand<T> and CommandHandler.
- Pepper 
- Password mixing comand that combines passwor, salt and pepper.
- Password hasing service, that hashes password using SHA256.
- SaltProvider class, that provides salt from database.
- SaltService that creates and saves new salt for provided user.
- Tests for all new classes.

## [0.1.0] - 18.03.2023 - JAA-1-SetUpDatabaseWithUserAndSaltTables

### Added
- EntityFramework Nuget package with related packages (EntityFrameworkCore.Design and EntityFrameworkCore.SqlServer)
- User identity and password salt model
- User identity and password salt databases contexts
- Registered databases contexts in application
- Added Local installation tutorial in Readme.md 

### Changed
- Updated Readme.md to include new nuget packages
- WeatherForecastController is temporarily used as a test controller

### Removed
- WeatherForecast class

## [0.0.0] - 16.03.2023 - JAA-0-SetUpSolutionAndProjects

### Added
- JwtAuthenticationApi project as ASP.NET Core Web API based on .NET 6
- Unit test project based on NUnit test framework.
- Nuget packages for test solution.
- Database connection string that are provided using IOptions pattern.