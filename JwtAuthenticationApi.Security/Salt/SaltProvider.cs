namespace JwtAuthenticationApi.Security.Salt
{
	using JwtAuthenticationApi.Common.Models;
	using JwtAuthenticationApi.Security.Abstraction.Salt;

	/// <summary>
	/// Implementation of <see cref="ISaltProvider"/>. Provides method for getting password salt.
	/// </summary>
	internal sealed class SaltProvider: ISaltProvider
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
		public async Task<string> GetPasswordSaltAsync(int userId, CancellationToken cancellationToken)
		{
			Result<string> saltFromDatabase = await _saltService.GetSaltAsync(userId, cancellationToken);
			if (saltFromDatabase.IsSuccessful)
			{
				return saltFromDatabase.Value;
			}

			var salt = _saltService.GenerateSalt();
			await _saltService.SaveSaltAsync(salt, userId, cancellationToken);
			return salt;
		}
	}
}