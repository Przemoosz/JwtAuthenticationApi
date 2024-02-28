namespace JwtAuthenticationApi.Infrastructure.Entities
{
	using Abstraction.Entity;

	public class UserEntity: EntityBase
	{
		/// <summary>
		/// Gets or sets username.
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// Gets or sets user hashed password.
		/// </summary>
		public string HashedPassword { get; set; }

		/// <summary>
		/// Gets or sets user email.
		/// </summary>
		public string Email { get; set; }


		/// <summary>
		/// Gets <see cref="DateTime"/> value whether this user was created.
		/// </summary>
		public DateTime CreationDate { get; init; }

		/// <summary>
		/// Initializes <see cref="UserEntity"/>.
		/// </summary>
		/// <param name="username">User name.</param>
		/// <param name="hashedPassword">Hashed user password.</param>
		/// <param name="email">User email.</param>
		public UserEntity(string username, string hashedPassword, string email)
		{
			Username = username;
			HashedPassword = hashedPassword;
			Email = email;
			CreationDate = DateTime.UtcNow;
		}
	}
}
