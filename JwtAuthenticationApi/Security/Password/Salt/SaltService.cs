﻿namespace JwtAuthenticationApi.Security.Password.Salt
{
	using Commands.Models;
	using Wrappers;
	using Microsoft.EntityFrameworkCore;
	using DatabaseContext;
	using Models;
	using Factories.Wrappers;
	using Wrappers.Threading;
	using ILogger = Serilog.ILogger;
	using Factories.Polly;
	using Polly;

	/// <inheritdoc/>
	public sealed class SaltService: ISaltService
	{
		private const int SemaphoreInitialCount = 1;
		private const int SemaphoreMaximalCount = 1;

		private readonly IPasswordSaltContext _context;
		private readonly IGuidWrapper _guidWrapper;
		private readonly IPollySleepingIntervalsFactory _pollySleepingIntervalsFactory;
		private readonly ISemaphoreWrapperFactory _semaphoreWrapperFactory;
		private readonly ILogger _logger;

		/// <summary>
		/// Initializes new instance of <see cref="SaltService"/> class.
		/// </summary>
		/// <param name="context">Password salt database context.</param>
		/// <param name="guidWrapper">Guid wrapper.</param>
		/// <param name="pollySleepingIntervalsFactory">Factory for sleeping interval used in Polly.</param>
		/// <param name="semaphoreWrapperFactory">Semaphore wrapper factory.</param>
		/// <param name="logger">Logger.</param>
		public SaltService(IPasswordSaltContext context, IGuidWrapper guidWrapper,
			IPollySleepingIntervalsFactory pollySleepingIntervalsFactory, 
			ISemaphoreWrapperFactory semaphoreWrapperFactory,
			ILogger logger)
		{
			_context = context;
			_guidWrapper = guidWrapper;
			_pollySleepingIntervalsFactory = pollySleepingIntervalsFactory;
			_semaphoreWrapperFactory = semaphoreWrapperFactory;
			_logger = logger;
		}

		/// <inheritdoc/>
		public async Task<string> CreateAndSaveSaltAsync(UserModel user, CancellationToken cancellationToken = new CancellationToken())
		{
			// IMutexWrapper mutexLock = _mutexWrapperFactory.Create(false, user.Id.ToString());
			ISemaphoreWrapper semaphore = _semaphoreWrapperFactory.Create(SemaphoreInitialCount, SemaphoreMaximalCount, user.Id.ToString());
			_logger.Warning($"Trying to acquire lock in {nameof(CreateAndSaveSaltAsync)} method with lock id: {user.Id}.");
			semaphore.WaitOne();
			// mutexLock.WaitOne();
			_logger.Warning($"Lock acquired in {nameof(CreateAndSaveSaltAsync)} method with lock id: {user.Id}.");
			var dbSavePolicy = Policy
				.Handle<DbUpdateException>()
				.Or<DbUpdateConcurrencyException>()
				.WaitAndRetryAsync(_pollySleepingIntervalsFactory.CreateLinearInterval(2,2,3), (exception, span) => _logger.Error($"Error occurred during execution of {nameof(GetSaltAsync)}. Attempting to retry in {span.Seconds} seconds. Error Message: {exception.Message}."));
			string salt = _guidWrapper.CreateGuid().ToString();
			PasswordSaltModel passwordSaltContext = new PasswordSaltModel(){Salt = salt, UserId = user.Id};
			try
			{
				await _context.PasswordSalt.AddAsync(passwordSaltContext, cancellationToken);
				await dbSavePolicy.ExecuteAsync(async () =>  await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false)).ConfigureAwait(false);
				_logger.Information($"Password salt saved for user {user.Id}.");
			}
			catch (DbUpdateException)
			{
				_logger.Error($"Failed to save password salt in database for user: {user.Id}.");
				throw;
			}
			finally
			{
				semaphore.Release();
				_logger.Warning($"Lock released (Lock id: {user.Id}) in {nameof(CreateAndSaveSaltAsync)} method.");
			}
			return salt;
		}

		/// <inheritdoc/>
		public async Task<Result<string>> GetSaltAsync(UserModel user, CancellationToken cancellationToken = new CancellationToken())
		{
			ISemaphoreWrapper semaphore = _semaphoreWrapperFactory.Create(SemaphoreInitialCount, SemaphoreMaximalCount, user.Id.ToString());
			_logger.Warning($"Trying to acquire lock in {nameof(GetSaltAsync)} method with lock id: {user.Id}.");
			semaphore.WaitOne();
			PasswordSaltModel passwordSaltModel = null;
			try
			{
				_logger.Warning($"Lock acquired in {nameof(GetSaltAsync)} method with lock id: {user.Id}.");

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
						.FirstOrDefaultAsync(u => u.UserId.Equals(user.Id), ct)
						.ConfigureAwait(false), cancellationToken, false).ConfigureAwait(false);
				passwordSaltModel = a;
			}
			catch (Exception ex)
			{
				_logger.Error(ex,$"Error occured during receiving password salt for user {user.Id}.");
				passwordSaltModel = null;
			}
			finally
			{
				semaphore.Release();
				_logger.Warning($"Lock released (Lock id: {user.Id}) in {nameof(GetSaltAsync)} method.");
			}
			if (passwordSaltModel is null)
			{
				_logger.Warning($"Received password salt for user: {user.Id} is null or empty.");
				return new Result<string>(null, false);
			}
			_logger.Information($"Received password salt for user: {user.Id}");
			return new Result<string>(passwordSaltModel.Salt, true);
		}
	}
}