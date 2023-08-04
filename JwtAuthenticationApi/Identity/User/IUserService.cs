using JwtAuthenticationApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthenticationApi.Identity.User;

/// <summary>
/// Defines method for saving user in database.
/// </summary>
public interface IUserService
{
	/// <summary>
	/// Saves provided user in database, and returns its identifier in database.
	/// </summary>
	/// <param name="userEntity">User entity which will be saved in database.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <exception cref="DbUpdateException"/>
	/// <returns>User identifier in database, value is <see cref="int"/> or <see langword="null"/>.</returns>
	Task<int?> SaveUserAsync(UserEntity userEntity, CancellationToken cancellationToken);

	/// <summary>
	/// Determines if user exists in database.
	/// </summary>
	/// <remarks>Its more efficient to use overload with userId if you already know user id.</remarks>
	/// <param name="userName">Username for which database will be queried.</param>
	/// <returns><see langword="true"/> if user exists. Otherwise <see langword="false"/>.</returns>
	Task<bool> UserExistsAsync(string userName);

	/// <summary>
	/// Determines if user exists in database.
	/// </summary>
	/// <param name="userId">User id for which database will be queried.</param>
	/// <returns><see langword="true"/> if user exists. Otherwise <see langword="false"/>.</returns>
	Task<bool> UserExistsAsync(int userId);
}