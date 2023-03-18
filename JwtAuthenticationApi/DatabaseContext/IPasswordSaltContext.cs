namespace JwtAuthenticationApi.DatabaseContext
{
	using Abstraction.DatabaseContext;
	using Models;
	using Microsoft.EntityFrameworkCore;

	public interface IPasswordSaltContext : IContext
	{
		DbSet<PasswordSaltModel> PasswordSalt { get; }
	}
}