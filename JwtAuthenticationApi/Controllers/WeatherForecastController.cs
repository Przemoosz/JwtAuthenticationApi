using JwtAuthenticationApi.DatabaseContext;
using JwtAuthenticationApi.Models;
using JwtAuthenticationApi.Models.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace JwtAuthenticationApi.Controllers
{


	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly ILogger<WeatherForecastController> _logger;
		private readonly IOptions<DatabaseConnectionStrings> _options;
		private readonly IUserContext _userContext;
		private readonly IPasswordSaltContext _passwordSaltContext;

		public WeatherForecastController(ILogger<WeatherForecastController> logger,
			IOptions<DatabaseConnectionStrings> options, IUserContext userContext,
			IPasswordSaltContext passwordSaltContext)
		{
			_logger = logger;
			_options = options;
			_userContext = userContext;
			_passwordSaltContext = passwordSaltContext;
		}

		[HttpGet(Name = "GetWeatherForecast")]
		public async Task Get()
		{
			Console.WriteLine(_options.Value.IdentityDatabaseConnectionString);
			Console.WriteLine(_options.Value.SaltDatabaseConnectionString);
			await _userContext.Users.AddAsync(new UserModel(){Id = Guid.NewGuid(), CreationDate = DateTime.UtcNow, Email = "dd", HashedPassword = "hashed", UserName = "type"});
			await _passwordSaltContext.PasswordSalt.AddAsync(new PasswordSaltModel()
				{ Id = Guid.NewGuid(), Salt = "salt", UserId = Guid.NewGuid() });
			await _userContext.SaveChangesAsync();
			await _passwordSaltContext.SaveChangesAsync();
		}
	}
}