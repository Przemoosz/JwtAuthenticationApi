using JwtAuthenticationApi.Models.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace JwtAuthenticationApi.Controllers
{


	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;
		private readonly IOptions<DatabaseConnectionStrings> _options;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptions<DatabaseConnectionStrings> options)
		{
			_logger = logger;
			_options = options;
		}

		[HttpGet(Name = "GetWeatherForecast")]
		public IEnumerable<WeatherForecast> Get()
		{
			Console.WriteLine(_options.Value.IdentityDatabaseConnectionString);
			Console.WriteLine(_options.Value.SaltDatabaseConnectionString);


			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}
	}
}