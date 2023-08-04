namespace JwtAuthenticationApi.Factories.Password
{
	using Models.Password;

	/// <summary>
	/// Defines method for password context factory.
	/// </summary>
	public interface IPasswordContextFactory
	{
		/// <summary>
		/// Creates <see cref="PasswordContext"/> from provided password and password confirmation.
		/// </summary>
		/// <param name="password">Password.</param>
		/// <param name="passwordConfirmation">Password confirmation.</param>
		/// <returns></returns>
		PasswordContext Create(string password, string passwordConfirmation);
	}
}