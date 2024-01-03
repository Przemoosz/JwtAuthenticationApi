namespace JwtAuthenticationApi.Abstraction.DatabaseContext
{
    using Microsoft.EntityFrameworkCore;
    using Entities;

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