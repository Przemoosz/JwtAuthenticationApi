namespace JwtAuthenticationApi.Entities
{
	using Abstraction.Entity;
	using System.ComponentModel.DataAnnotations.Schema;

	public class UserEntity: EntityBase
	{
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

		public UserEntity(string userName, string hashedPassword, string email)
		{
			UserName = userName;
			HashedPassword = hashedPassword;
			Email = email;
			CreationDate = DateTime.Now;
		}
	}
}
