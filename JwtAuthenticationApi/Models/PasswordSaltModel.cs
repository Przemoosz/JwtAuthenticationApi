namespace JwtAuthenticationApi.Models
{
	using System.ComponentModel.DataAnnotations;
	using Abstraction.Models;
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// Password salt model, that will be saved in database. Inherits <see cref="ModelBase"/>.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public sealed class PasswordSaltModel: ModelBase
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
		public Guid UserId { get; init; }
	}
}