namespace JwtAuthenticationApi.Factories.Password
{
	using Models.Password;

	public interface IPasswordContextFactory
	{
		PasswordContext Create(string password, string passwordConfirmation);
	}
}