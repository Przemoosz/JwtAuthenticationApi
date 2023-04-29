using JwtAuthenticationApi.Factories.Polly;
using JwtAuthenticationApi.Factories.Wrappers;
using JwtAuthenticationApi.Security.Password.Salt;
using JwtAuthenticationApi.Wrappers;
using JwtAuthenticationApi.Wrappers.Threading;

namespace JwtAuthenticationApi.Container
{
	using DatabaseContext;
	using Models.Options;
	using Microsoft.EntityFrameworkCore;
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// Defines extensions methods for container setup.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static class ContainerSetup
	{
		/// <summary>
		/// Register options.
		/// </summary>
		/// <param name="builder">Web application builder</param>
		public static void RegisterOptions(this WebApplicationBuilder builder)
		{
			builder.Services.Configure<DatabaseConnectionStrings>(
				builder.Configuration.GetSection(nameof(DatabaseConnectionStrings)));
			builder.Services.Configure<PasswordPepper>(builder.Configuration.GetSection(nameof(PasswordPepper)));
		}

		/// <summary>
		/// Register user identity database context.
		/// </summary>
		/// <param name="builder">Web application builder</param>
		public static void RegisterUserIdentityDatabaseContext(this WebApplicationBuilder builder)
		{
			builder.Services.AddDbContext<IUserContext, UserContext>(options =>
			{
				options.UseSqlServer(builder.Configuration
					.GetSection(
						$"{nameof(DatabaseConnectionStrings)}:{nameof(DatabaseConnectionStrings.IdentityDatabaseConnectionString)}")
					.Value);
			});
		}

		/// <summary>
		/// Register password salt database context.
		/// </summary>
		/// <param name="builder">Web application builder</param>
		public static void RegisterPasswordSaltDatabaseContext(this WebApplicationBuilder builder)
		{
			builder.Services.AddDbContext<IPasswordSaltContext, PasswordSaltContext>(options =>
			{
				options.UseSqlServer(builder.Configuration
					.GetSection(
						$"{nameof(DatabaseConnectionStrings)}:{nameof(DatabaseConnectionStrings.SaltDatabaseConnectionString)}")
					.Value);
			});
		}

		public static void RegisterServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddTransient<ISaltProvider, SaltProvider>();
			builder.Services.AddTransient<ISaltService, SaltService>();
			builder.Services.AddTransient<IGuidWrapper, GuidWrapper>();
			builder.Services.AddTransient<IMutexWrapperFactory, MutexWrapperFactory>();
			builder.Services.AddTransient<IPollySleepingIntervalsFactory, PollySleepingIntervalsFactory>();
		}
	}
}