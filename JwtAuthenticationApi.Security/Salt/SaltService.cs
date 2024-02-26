namespace JwtAuthenticationApi.Security.Salt
{
    using JwtAuthenticationApi.Common.Abstraction.Factories.Wrappers;
    using JwtAuthenticationApi.Common.Abstraction.Wrappers.Threading;
    using Common.Models;
    using JwtAuthenticationApi.Common.Abstraction.Factories.Polly;
    using JwtAuthenticationApi.Infrastructure.Abstraction.Database;
    using Infrastructure.Entities;
    using JwtAuthenticationApi.Security.Abstraction.Salt;
    using Microsoft.EntityFrameworkCore;
    using ILogger = Serilog.ILogger;
    using Polly;
    using JwtAuthenticationApi.Common.Abstraction.Factories;

	/// <inheritdoc/>
	internal sealed class SaltService: ISaltService
	{
		private const int SemaphoreInitialCount = 1;
		private const int SemaphoreMaximalCount = 1;

		private readonly IPasswordSaltContext _context;
		private readonly IGuidFactory _guidFactory;
		private readonly IPollySleepingIntervalsFactory _pollySleepingIntervalsFactory;
		private readonly ISemaphoreWrapperFactory _semaphoreWrapperFactory;
		private readonly ILogger _logger;

		/// <summary>
		/// Initializes new instance of <see cref="SaltService"/> class.
		/// </summary>
		/// <param name="context">Password salt database context.</param>
		/// <param name="guidFactory">Guid wrapper.</param>
		/// <param name="pollySleepingIntervalsFactory">Factory for sleeping interval used in Polly.</param>
		/// <param name="semaphoreWrapperFactory">Semaphore wrapper factory.</param>
		/// <param name="logger">Logger.</param>
		public SaltService(IPasswordSaltContext context, IGuidFactory guidFactory,
			IPollySleepingIntervalsFactory pollySleepingIntervalsFactory, 
			ISemaphoreWrapperFactory semaphoreWrapperFactory,
			ILogger logger)
		{
			_context = context;
			_guidFactory = guidFactory;
			_pollySleepingIntervalsFactory = pollySleepingIntervalsFactory;
			_semaphoreWrapperFactory = semaphoreWrapperFactory;
			_logger = logger;
		}
		public string GenerateSalt() => _guidFactory.CreateGuid().ToString();

		/// <inheritdoc/>
		public async Task<int?> SaveSaltAsync(string salt, int userId, CancellationToken cancellationToken = new CancellationToken())
		{
			ISemaphoreWrapper semaphore = _semaphoreWrapperFactory.Create(SemaphoreInitialCount, SemaphoreMaximalCount, userId.ToString());
			_logger.Warning($"Trying to acquire lock in {nameof(SaveSaltAsync)} method with lock id: {userId}.");
			semaphore.WaitOne();

			_logger.Warning($"Lock acquired in {nameof(SaveSaltAsync)} method with lock id: {userId}.");
			var dbSavePolicy = Policy
				.Handle<DbUpdateException>()
				.Or<DbUpdateConcurrencyException>()
				.WaitAndRetryAsync(_pollySleepingIntervalsFactory.CreateLinearInterval(2,2,3), (exception, span) => _logger.Error($"Error occurred during execution of {nameof(GetSaltAsync)}. Attempting to retry in {span.Seconds} seconds. Error Message: {exception.Message}."));
			PasswordSaltEntity passwordSaltContext = new PasswordSaltEntity(salt, userId);
			int? id;
			try
			{
				await _context.PasswordSalt.AddAsync(passwordSaltContext, cancellationToken);
				 id = await dbSavePolicy.ExecuteAsync(async () =>  await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false)).ConfigureAwait(false);
				_logger.Information($"Password salt saved for user {userId}.");
			}
			catch (DbUpdateException ex)
			{
				_logger.Error(ex,$"Failed to save password salt in database for user: {userId}.");
				throw;
			}
			finally
			{
				semaphore.Release();
				_logger.Warning($"Lock released (Lock id: {userId}) in {nameof(SaveSaltAsync)} method.");
			}

			return id;
		}

		/// <inheritdoc/>
		public async Task<Result<string>> GetSaltAsync(int userId, CancellationToken cancellationToken = new CancellationToken())
		{
			ISemaphoreWrapper semaphore = _semaphoreWrapperFactory.Create(SemaphoreInitialCount, SemaphoreMaximalCount, userId.ToString());
			_logger.Warning($"Trying to acquire lock in {nameof(GetSaltAsync)} method with lock id: {userId}.");
			semaphore.WaitOne();
			PasswordSaltEntity passwordSaltModel = null;
			try
			{
				_logger.Warning($"Lock acquired in {nameof(GetSaltAsync)} method with lock id: {userId}.");

				var policy = Policy.Handle<Exception>()
					.WaitAndRetryAsync(_pollySleepingIntervalsFactory.CreateLinearInterval(2, 2, 3),
						(x, z) =>
						{
							_logger.Error(
								$"Error occurred during execution of {nameof(GetSaltAsync)}. Attempting to retry in {z.Seconds} seconds. Error Message: {x.Message}.");
							_logger.Error($"#ManagedThread# - {Thread.CurrentThread.ManagedThreadId}");
						});

				var a = await policy.ExecuteAsync(
					async (ct) => await _context
						.PasswordSalt
						.FirstOrDefaultAsync(u => u.UserId.Equals(userId), ct)
						.ConfigureAwait(false), cancellationToken, false).ConfigureAwait(false);
				passwordSaltModel = a;
			}
			catch (Exception ex)
			{
				_logger.Error(ex,$"Error occured during receiving password salt for user {userId}.");
				passwordSaltModel = null;
			}
			finally
			{
				semaphore.Release();
				_logger.Warning($"Lock released (Lock id: {userId}) in {nameof(GetSaltAsync)} method.");
			}
			if (passwordSaltModel is null)
			{
				_logger.Warning($"Received password salt for user: {userId} is null or empty.");
				return new Result<string>(null, false);
			}
			_logger.Information($"Received password salt for user: {userId}");
			return new Result<string>(passwordSaltModel.Salt, true);
		}
	}
}