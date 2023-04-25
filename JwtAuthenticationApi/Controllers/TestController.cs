using ILogger = Serilog.ILogger;

namespace JwtAuthenticationApi.Controllers
{
	using DatabaseContext;
	using Models;
	using Models.Options;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Options;
	using System.Diagnostics.CodeAnalysis;

	[ApiController]
	[Route("[controller]")]
	[ExcludeFromCodeCoverage]
	public class TestController : ControllerBase
	{
		private readonly ILogger<TestController> _logger;
		private readonly IOptions<DatabaseConnectionStrings> _options;
		private readonly IUserContext _userContext;
		private readonly IPasswordSaltContext _passwordSaltContext;
		private readonly IOptions<PasswordPepper> _passwordPepperOptions;
		private readonly ILogger _seriLogger;

		public TestController(ILogger<TestController> logger,
			IOptions<DatabaseConnectionStrings> options, IUserContext userContext,
			IPasswordSaltContext passwordSaltContext, IOptions<PasswordPepper> passwordPepperOptions, ILogger seriLogger)
		{
			_logger = logger;
			_options = options;
			_userContext = userContext;
			_passwordSaltContext = passwordSaltContext;
			_passwordPepperOptions = passwordPepperOptions;
			_seriLogger = seriLogger;
		}

		[HttpGet("GetTestData")]
		public async Task Get()
		{
			_seriLogger.Error("fff");
			throw new NullReferenceException();
			// Console.WriteLine(_passwordPepperOptions.Value.Pepper);
			// // Console.WriteLine(_options.Value.IdentityDatabaseConnectionString);
			// // Console.WriteLine(_options.Value.SaltDatabaseConnectionString);
			// await _userContext.Users.AddAsync(new UserModel(){Id = Guid.NewGuid(), CreationDate = DateTime.UtcNow, Email = "dd", HashedPassword = "hashed", UserName = "type"});
			// // await _passwordSaltContext.PasswordSalt.AddAsync(new PasswordSaltModel()
			// // 	{ Id = Guid.NewGuid(), Salt = "salt", UserId = Guid.NewGuid() });
			// await _userContext.SaveChangesAsync();
			// await _passwordSaltContext.SaveChangesAsync();
		}

		[HttpGet("TestBlocking")]
		public async Task GetTestBlocking()
		{
			Console.WriteLine($"hi from thread - {Thread.CurrentThread.ManagedThreadId}");
			Thread.Sleep(4000);
			Console.WriteLine("Done");
			Thread.Sleep(1000);
		}
	}
}