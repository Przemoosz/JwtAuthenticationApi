namespace JwtAuthenticationApi.Abstraction.DatabaseContext
{
	using Microsoft.EntityFrameworkCore;
    using Entities;

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