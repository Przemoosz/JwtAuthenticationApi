namespace JwtAuthenticationApi
{
    using Container;
    using System.Diagnostics.CodeAnalysis;
    using Container.Logger;


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
			builder.RegisterOptions();
			builder.RegisterUserIdentityDatabaseContext();
			builder.RegisterPasswordSaltDatabaseContext();
			builder.SetupSerilog();
			builder.RegisterServices();
			builder.RegisterSecurityServices();
			builder.RegisterFactories();
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
	}
}