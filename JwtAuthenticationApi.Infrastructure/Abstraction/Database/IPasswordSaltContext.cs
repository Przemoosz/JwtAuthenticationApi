namespace JwtAuthenticationApi.Infrastructure.Abstraction.Database
{
	using Entities;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	/// Defines properties for password salt database context.
	/// </summary>
	public interface IPasswordSaltContext : IContext
    {
        /// <summary>
        /// Gets password salt database set.
        /// </summary>
        DbSet<PasswordSaltEntity> PasswordSalt { get; }
    }
}