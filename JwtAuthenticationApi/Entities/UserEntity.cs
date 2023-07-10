using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtAuthenticationApi.Entities
{
	[Obsolete]
	public class UserEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // generate for insert new only
		public Guid Id { get; set; }
		/// <summary>
		/// Gets or sets username.
		/// </summary>
		public string UserName { get; set; }

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

		// public UserEntity(Guid id, string userName, string hashedPassword, string email) : base(id)
		// {
		// 	UserName = userName;
		// 	HashedPassword = hashedPassword;
		// 	Email = email;
		// 	CreationDate = DateTime.UtcNow;
		// }
	}
}
