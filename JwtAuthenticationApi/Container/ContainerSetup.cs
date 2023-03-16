using JwtAuthenticationApi.Models.Options;

namespace JwtAuthenticationApi.Container
{
	public static class ContainerSetup
	{
		public static void RegisterOptions(this WebApplicationBuilder builder)
		{
			builder.Services.Configure<DatabaseConnectionStrings>(
				builder.Configuration.GetSection(nameof(DatabaseConnectionStrings)));
		}
	}
}
