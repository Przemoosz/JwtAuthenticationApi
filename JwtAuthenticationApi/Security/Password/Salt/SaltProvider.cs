namespace JwtAuthenticationApi.Security.Password.Salt
{
	using Commands.Models;
	using Models;

	/// <summary>
	/// Implementation of <see cref="ISaltProvider"/>. Provides method for getting password salt.
	/// </summary>
	public sealed class SaltProvider: ISaltProvider
	{
		private readonly ISaltService _saltService;

		/// <summary>
		/// Initializes new instance of <see cref="SaltProvider"/> class.
		/// </summary>
		/// <param name="saltService">Salt service.</param>
		public SaltProvider(ISaltService saltService)
		{
			_saltService = saltService;
		}
		
		/// <inheritdoc/>
		public async Task<string> GetPasswordSaltAsync(Guid userId, CancellationToken cancellationToken)
		{
			Result<string> saltFromDatabase = await _saltService.GetSaltAsync(userId, cancellationToken);
			if (saltFromDatabase.IsSuccessful)
			{
				return saltFromDatabase.Value;
			}

			string createdSalt = await _saltService.CreateAndSaveSaltAsync(userId, cancellationToken);
			return createdSalt;
		}
	}
}