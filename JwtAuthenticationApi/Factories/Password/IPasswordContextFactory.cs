namespace JwtAuthenticationApi.Factories.Password
{
	using Models.Password;

	/// <summary>
	/// Define
	/// </summary>
	public interface IPasswordContextFactory
	{
		PasswordContext Create(string password, string passwordConfirmation);
	}
}