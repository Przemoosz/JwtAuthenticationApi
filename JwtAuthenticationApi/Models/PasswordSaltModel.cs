namespace JwtAuthenticationApi.Models
{
	using System.ComponentModel.DataAnnotations;

	using Abstraction.Models;

	public sealed class PasswordSaltModel: ModelBase
	{
		[Required]
		public string Salt { get; set; }
		[Required]
		public Guid UserId { get; init; }
	}
}
