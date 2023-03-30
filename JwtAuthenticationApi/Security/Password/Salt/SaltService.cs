using JwtAuthenticationApi.Factories.Wrappers;
using JwtAuthenticationApi.Wrappers.Threading;

namespace JwtAuthenticationApi.Security.Password.Salt
{
	using Commands.Models;
	using Wrappers;
	using Microsoft.EntityFrameworkCore;
	using DatabaseContext;
	using Models;

	public sealed class SaltService: ISaltService
	{
		private readonly IPasswordSaltContext _context;
		private readonly IGuidWrapper _guidWrapper;
		private readonly IMutexWrapperFactory _mutexWrapperFactory;

		public SaltService(IPasswordSaltContext context, IGuidWrapper guidWrapper, IMutexWrapperFactory mutexWrapperFactory)
		{
			_context = context;
			_guidWrapper = guidWrapper;
			_mutexWrapperFactory = mutexWrapperFactory;
		}

		// TODO MUTEX
		public async Task<string> CreateAndSaveSaltAsync(UserModel user, CancellationToken cancellationToken = new CancellationToken())
		{
			IMutexWrapper mutexLock = _mutexWrapperFactory.Create(true, user.Id.ToString());
			mutexLock.WaitOne();
			string salt = _guidWrapper.CreateGuid().ToString();
			PasswordSaltModel passwordSaltContext = new PasswordSaltModel(){Salt = salt, UserId = user.Id};
			try
			{
				await _context.PasswordSalt.AddAsync(passwordSaltContext, cancellationToken);
				await _context.SaveChangesAsync(cancellationToken);
			}
			catch (DbUpdateException ex)
			{

				throw;
			}
			finally
			{
				mutexLock.ReleaseMutex();
			}
			return salt;
		}

		public async Task<Result<string>> GetSaltAsync(UserModel user, CancellationToken cancellationToken = new CancellationToken())
		{
			PasswordSaltModel passwordSaltModel = await _context.PasswordSalt.FirstOrDefaultAsync(u => u.UserId.Equals(user.Id), cancellationToken);
			if (passwordSaltModel is null)
			{
				return new Result<string>(null, false);
			}
			return new Result<string>(passwordSaltModel.Salt, true);
		}
	}
}