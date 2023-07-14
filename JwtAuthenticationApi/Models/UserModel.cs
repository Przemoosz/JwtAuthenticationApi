namespace JwtAuthenticationApi.Models
{
	using Abstraction.Models;
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// User model, that will be saved in database. Inherits <see cref="ModelBase"/>.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public sealed class UserModel: ModelBase
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

		public UserModel(int id, string userName, string hashedPassword, string email) : base(id)
		{
			UserName = userName;
			HashedPassword = hashedPassword;
			Email = email;
			CreationDate = DateTime.UtcNow;
		}
	}
}