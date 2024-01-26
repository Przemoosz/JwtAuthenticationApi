namespace JwtAuthenticationApi
{
    using Container;
    using System.Diagnostics.CodeAnalysis;
    using Common;
    using Container.Logger;
    using Infrastructure;
    using Options;
    using Security;
    using Services;


    [ExcludeFromCodeCoverage]
	internal static class JwtAuthenticationApi
	{
		public static async Task Main(string[] args)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllers(s =>
			{
				s.ReturnHttpNotAcceptable = true;
			}); 
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.InstallCommon();
			builder.Services.InstallSecurity();
			builder.Services.InstallServices();
			builder.Services.InstallInfrastructureProject(builder.GetIdentityConnectionString, builder.GetSaltConnectionString);

			// builder.RegisterOptions();
			// builder.RegisterUserIdentityDatabaseContext();
			// builder.RegisterPasswordSaltDatabaseContext();
			builder.SetupSerilog();
			// builder.RegisterServices();
			// builder.RegisterSecurityServices();
			// builder.RegisterFactories();
			var app = builder.Build();
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}  
			app.UseHttpsRedirection();
			
			app.UseAuthentication();
			app.UseAuthorization();
			
			
			app.MapControllers();
			
			await app.RunAsync();
		}


		private static string GetSaltConnectionString(this WebApplicationBuilder webApplicationBuilder)
		{
			return webApplicationBuilder.Configuration.GetSection(
					$"{nameof(DatabaseConnectionStrings)}:{nameof(DatabaseConnectionStrings.SaltDatabaseConnectionString)}").Value;
		}

		private static string GetIdentityConnectionString(this WebApplicationBuilder webApplicationBuilder)
		{
			return webApplicationBuilder.Configuration.GetSection(
				$"{nameof(DatabaseConnectionStrings)}:{nameof(DatabaseConnectionStrings.IdentityDatabaseConnectionString)}").Value;
		}

	}
}