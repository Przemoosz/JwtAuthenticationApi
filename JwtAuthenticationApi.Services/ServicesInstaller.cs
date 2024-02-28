namespace JwtAuthenticationApi.Services
{
	using Abstraction.Factories.Password;
	using Abstraction.Identity.User;
	using Abstraction.Registration;
	using Abstraction.RuleEngine;
	using Abstraction.Validators.Password;
	using Factories.Password;
	using Identity.User;
	using Microsoft.Extensions.DependencyInjection;
	using Registration;
	using Validators.Password;

	public static class ServicesInstaller
	{
		public static void InstallServices(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddTransient<IUserService, UserService>();
			serviceCollection.AddTransient(typeof(IRuleEngine<>), typeof(RuleEngine<>));
			serviceCollection.AddTransient<IUserRegisterService, UserRegisterService>();
			serviceCollection.AddTransient<IPasswordContextFactory, PasswordContextFactory>();
			serviceCollection.AddTransient<IPasswordRuleFactory, PasswordRuleFactory>();
			serviceCollection.AddTransient<IPasswordValidator, PasswordValidator>();
		}
	}
}
