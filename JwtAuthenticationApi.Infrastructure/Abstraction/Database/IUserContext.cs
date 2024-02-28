namespace JwtAuthenticationApi.Infrastructure.Abstraction.Database
{
	using Entities;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	/// Defines properties for user database context.
	/// </summary>
	public interface IUserContext : IContext
    {
        /// <summary>
        /// Gets users database set.
        /// </summary>
        DbSet<UserEntity> Users { get; }
    }
}