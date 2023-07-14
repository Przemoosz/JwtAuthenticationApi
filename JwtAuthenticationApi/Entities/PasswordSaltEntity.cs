namespace JwtAuthenticationApi.Entities
{
	using Abstraction.Entity;
	using System.ComponentModel.DataAnnotations;

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

		public PasswordSaltEntity(string salt, int userId)
		{
			Salt = salt;
			UserId = userId;
		}
	}
}
