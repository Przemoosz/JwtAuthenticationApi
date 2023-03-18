namespace JwtAuthenticationApi.DatabaseContext
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Abstraction.DatabaseContext;

	public interface IUserContext : IContext
	{
		DbSet<UserModel> Users { get; }
	}
}