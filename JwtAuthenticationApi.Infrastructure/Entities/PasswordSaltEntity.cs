namespace JwtAuthenticationApi.Infrastructure.Entities
{
	using System.ComponentModel.DataAnnotations;
	using Abstraction.Entity;

	public class PasswordSaltEntity: EntityBase
	{
		/// <summary>
		/// Gets or sets Password salt.
		/// </summary>
		[Required]
		public string Salt { get; set; }

		/// <summary>
		/// Gets <see cref="Guid"/> value of user that is associated with this salt.
		/// </summary>
		[Required]
		public int UserId { get; init; }

		/// <summary>
		/// Initializes <see cref="PasswordSaltEntity"/>.
		/// </summary>
		/// <param name="salt">Password salt.</param>
		/// <param name="userId">User identifier.</param>
		public PasswordSaltEntity(string salt, int userId)
		{
			Salt = salt;
			UserId = userId;
		}
	}
}
