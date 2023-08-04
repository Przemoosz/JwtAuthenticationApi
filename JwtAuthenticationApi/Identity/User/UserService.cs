namespace JwtAuthenticationApi.Identity.User
{
	using DatabaseContext;
	using Entities;
	using Factories.Polly;
	using ILogger = Serilog.ILogger;
	using Polly;
	using Polly.Retry;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	/// User service that is responsible for saving user in database. Implements <see cref="IUserService"/>.
	/// </summary>
	public class UserService: IUserService
	{
		private readonly IUserContext _userContext;
		private readonly IPollySleepingIntervalsFactory _pollySleepingIntervalsFactory;
		private readonly ILogger _logger;

		/// <summary>
		/// Returns instance of <see cref="UserService"/>.
		/// </summary>
		/// <param name="userContext">User context</param>
		/// <param name="pollySleepingIntervalsFactory">Polly sleeping intervals factory.</param>
		/// <param name="logger">Logger.</param>
		public UserService(IUserContext userContext, IPollySleepingIntervalsFactory pollySleepingIntervalsFactory, ILogger logger)
		{
			_userContext = userContext;
			_pollySleepingIntervalsFactory = pollySleepingIntervalsFactory;
			_logger = logger;
		}

		public async Task<int?> SaveUserAsync(UserEntity userEntity, CancellationToken cancellationToken)
		{
			AsyncRetryPolicy<int> saveUserInDatabasePolicy = Policy<int>
				.Handle<DbUpdateException>()
				.WaitAndRetryAsync(_pollySleepingIntervalsFactory.CreateLinearInterval(4, 1, 3),
					(exception, span) =>
						_logger.Error(
							$"Error occurred during execution of {nameof(SaveUserAsync)}. Attempting to retry in {span.Seconds} seconds. Error Message: {exception.Exception.Message}."));
			_logger.Information("Attempting to save user in database");
			await _userContext.Users.AddAsync(userEntity, cancellationToken);
			await saveUserInDatabasePolicy.ExecuteAsync(async () => await _userContext.SaveChangesAsync(cancellationToken));
			_logger.Information("User saved in database");
			return userEntity.Id;
		}

		public async Task<bool> UserExistsAsync(string userName)
		{
			var result = await _userContext.Users.AnyAsync(e => e.Username.Equals(userName));
			return result;
		}
		public async Task<bool> UserExistsAsync(int userId)
		{
			var result = await _userContext.Users.AnyAsync(e => e.Id.Equals(userId));
			return result;
		}
	}
}
