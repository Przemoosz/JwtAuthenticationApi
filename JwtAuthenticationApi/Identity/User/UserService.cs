namespace JwtAuthenticationApi.Identity.User
{
	using DatabaseContext;
	using Entities;
	using Factories.Polly;
	using ILogger = Serilog.ILogger;
	using Polly;
	using Polly.Retry;
	using Microsoft.EntityFrameworkCore;

	public class UserService: IUserService
	{

		private readonly IUserContext _userContext;
		private readonly IPollySleepingIntervalsFactory _pollySleepingIntervalsFactory;
		private readonly ILogger _logger;

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
				.Or<DbUpdateConcurrencyException>()
				.WaitAndRetryAsync(_pollySleepingIntervalsFactory.CreateLinearInterval(4, 1, 3),
					(exception, span) =>
						_logger.Error(
							$"Error occurred during execution of {nameof(SaveUserAsync)}. Attempting to retry in {span.Seconds} seconds. Error Message: {exception.Exception.Message}."));
			int? userId;
			try
			{
				_logger.Information("Attempting to save user in database");
				await _userContext.Users.AddAsync(userEntity, cancellationToken);
				userId = await saveUserInDatabasePolicy.ExecuteAsync(async () => await _userContext.SaveChangesAsync(cancellationToken));
				_logger.Information("User saved in database");
			}
			catch (DbUpdateException) // todo try catch and return error code
			{
				_logger.Error($"Failed to save user in database");
				throw;
			}
			return userId;
		}
	}

	public interface IUserService
	{
		Task<int?> SaveUserAsync(UserEntity userEntity, CancellationToken cancellationToken);

	}
}
