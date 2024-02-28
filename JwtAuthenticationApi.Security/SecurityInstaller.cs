namespace JwtAuthenticationApi.Security
{
	using Abstraction.Cryptography;
	using Abstraction.Salt;
	using Cryptography;
	using Microsoft.Extensions.DependencyInjection;
	using Salt;

	public static class SecurityInstaller
	{
		public static void InstallSecurity(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddTransient<IPasswordHashingService, PasswordHashingService>();
			serviceCollection.AddTransient<ISaltProvider, SaltProvider>();
			serviceCollection.AddTransient<ISaltService, SaltService>();
		}
	}
}
