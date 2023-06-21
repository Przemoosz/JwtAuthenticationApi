using JwtAuthenticationApi.Models.Password;

namespace JwtAuthenticationApi.DatabaseContext
{
    using Abstraction.DatabaseContext;
    using Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Defines properties for password salt database context.
    /// </summary>
    public interface IPasswordSaltContext : IContext
	{
		/// <summary>
		/// Gets password salt database set.
		/// </summary>
		DbSet<PasswordSaltModel> PasswordSalt { get; }
	}
}