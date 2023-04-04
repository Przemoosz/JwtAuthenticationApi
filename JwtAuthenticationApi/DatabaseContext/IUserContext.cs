namespace JwtAuthenticationApi.DatabaseContext
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Abstraction.DatabaseContext;

	/// <summary>
	/// Defines properties for user database context.
	/// </summary>
	public interface IUserContext : IContext
	{
		/// <summary>
		/// Gets users database set.
		/// </summary>
		DbSet<UserModel> Users { get; }
	}
}