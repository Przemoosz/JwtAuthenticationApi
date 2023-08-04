namespace JwtAuthenticationApi.Container
{
    using DatabaseContext;
    using Models.Options;
    using Microsoft.EntityFrameworkCore;
    using System.Diagnostics.CodeAnalysis;
    using Abstraction.RuleEngine;
    using Factories.Password;
    using Factories.Polly;
    using Factories.Wrappers;
    using Handlers;
    using Security.Password;
    using Security.Password.Salt;
    using Validators.Password;
    using Wrappers;
    using Registration;
    using Identity.User;
    using Factories.Commands;

	/// <summary>
	/// Defines extensions methods for container setup.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static class ContainerSetup
	{
		/// <summary>
		/// Register options.
		/// </summary>
		/// <param name="builder">Web application builder.</param>
		public static void RegisterOptions(this WebApplicationBuilder builder)
		{
			builder.Services.Configure<DatabaseConnectionStrings>(
				builder.Configuration.GetSection(nameof(DatabaseConnectionStrings)));
			builder.Services.Configure<PasswordPepper>(builder.Configuration.GetSection(nameof(PasswordPepper)));
		}

		/// <summary>
		/// Register user identity database context.
		/// </summary>
		/// <param name="builder">Web application builder.</param>
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
		/// <param name="builder">Web application builder.</param>
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

		/// <summary>
		/// Register common services.
		/// </summary>
		/// <param name="builder">Web application builder.</param>
		public static void RegisterServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddSingleton<IGuidWrapper, GuidWrapper>();
			builder.Services.AddTransient<ICommandHandler, CommandHandler>();
			builder.Services.AddTransient<IUserService, UserService>();
			builder.Services.AddTransient(typeof(IRuleEngine<>), typeof(RuleEngine<>));
			builder.Services.AddTransient<IUserRegisterService, UserRegisterService>();
		}

		/// <summary>
		/// Register security services.
		/// </summary>
		/// <param name="builder">Web application builder.</param>
		public static void RegisterSecurityServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddTransient<ISaltProvider, SaltProvider>();
			builder.Services.AddTransient<ISaltService, SaltService>();
			builder.Services.AddTransient<IPasswordValidator, PasswordValidator>();
			builder.Services.AddTransient<IPasswordHashingService, PasswordHashingService>();
			builder.Services.AddTransient<IPasswordContextFactory, PasswordContextFactory>();
		}

		/// <summary>
		/// Register factories.
		/// </summary>
		/// <param name="builder">Web application builder.</param>
		public static void RegisterFactories(this WebApplicationBuilder builder)
		{
			builder.Services.AddSingleton<ICommandFactory, CommandFactory>();
			builder.Services.AddSingleton<IPasswordRuleFactory, PasswordRuleFactory>();
			builder.Services.AddSingleton<IMutexWrapperFactory, MutexWrapperFactory>();
			builder.Services.AddSingleton<IPollySleepingIntervalsFactory, PollySleepingIntervalsFactory>();
			builder.Services.AddSingleton<ISemaphoreWrapperFactory, SemaphoreWrapperFactory>();
		}
	}
}