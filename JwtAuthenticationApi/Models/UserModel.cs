namespace JwtAuthenticationApi.Models
{
	using Abstraction.Models;
	using System.ComponentModel.DataAnnotations;
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
		[Required]
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets user hashed password.
		/// </summary>
		[Required]
		public string HashedPassword { get; set; }

		/// <summary>
		/// Gets or sets user email.
		/// </summary>
		[Required]
		public string Email { get; set; }

		/// <summary>
		/// Gets <see cref="DateTime"/> value whether this user was created.
		/// </summary>
		[Required]
		public DateTime CreationDate { get; init; }
	}
}