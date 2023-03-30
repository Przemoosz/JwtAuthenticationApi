namespace JwtAuthenticationApi.Security.Password.Salt
{
	using Commands.Models;
	using Models;

	public sealed class SaltProvider: ISaltProvider
	{
		private readonly ISaltService _saltService;

		public SaltProvider(ISaltService saltService)
		{
			_saltService = saltService;
		}

		public async Task<string> GetPasswordSaltAsync(UserModel user, CancellationToken cancellationToken)
		{
			Result<string> saltFromDatabase = await _saltService.GetSaltAsync(user, cancellationToken);
			if (saltFromDatabase.IsSuccessful)
			{
				return saltFromDatabase.Value;
			}

			string createdSalt = await _saltService.CreateAndSaveSaltAsync(user, cancellationToken);
			return createdSalt;
		}
	}
}