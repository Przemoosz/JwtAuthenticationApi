namespace JwtAuthenticationApi.Models
{
	using Abstraction.Models;
	using System.ComponentModel.DataAnnotations;

	public sealed class UserModel: ModelBase
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		public string HashedPassword { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public DateTime CreationDate { get; init; }
	}
}
