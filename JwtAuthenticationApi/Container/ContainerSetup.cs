namespace JwtAuthenticationApi.Container
{
	using DatabaseContext;
	using Models.Options;
	using Microsoft.EntityFrameworkCore;
	using System.Diagnostics.CodeAnalysis;

	[ExcludeFromCodeCoverage]
	public static class ContainerSetup
	{
		public static void RegisterOptions(this WebApplicationBuilder builder)
		{
			builder.Services.Configure<DatabaseConnectionStrings>(
				builder.Configuration.GetSection(nameof(DatabaseConnectionStrings)));
			builder.Services.Configure<PasswordPepper>(builder.Configuration.GetSection(nameof(PasswordPepper)));
		}

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
	}
}
