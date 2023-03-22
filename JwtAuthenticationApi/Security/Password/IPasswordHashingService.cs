﻿namespace JwtAuthenticationApi.Security.Password
{
	/// <summary>
	/// Defines method for password hashing.
	/// </summary>
	public interface IPasswordHashingService
	{
		/// <summary>
		/// Hashes provided password with pepper and salt using SHA256.
		/// </summary>
		/// <param name="password">Password.</param>
		/// <param name="salt">Password salt.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>Base64 hashed password.</returns>
		Task<string> HashAsync(string password, string salt, CancellationToken cancellationToken);
	}
}