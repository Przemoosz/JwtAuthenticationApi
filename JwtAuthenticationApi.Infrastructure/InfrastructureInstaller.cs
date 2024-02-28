namespace JwtAuthenticationApi.Infrastructure
{
	using JwtAuthenticationApi.Infrastructure.Abstraction.Database;
	using Database;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;

	public static class InfrastructureInstaller
	{
		public static void InstallInfrastructureProject(this IServiceCollection serviceCollection, Func<string> identityDatabaseConnectionStringFunc, Func<string> saltDatabaseConnectionStringFunc)
		{
			serviceCollection.AddDbContext<IUserContext, UserContext>(options =>
			{
				options.UseNpgsql(identityDatabaseConnectionStringFunc());
			});
			serviceCollection.AddDbContext<IPasswordSaltContext, PasswordSaltContext>(options =>
			{
				options.UseNpgsql(saltDatabaseConnectionStringFunc());
			});

		}
	}
}
