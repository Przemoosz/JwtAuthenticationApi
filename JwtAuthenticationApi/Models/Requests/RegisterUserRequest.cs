namespace JwtAuthenticationApi.Models.Requests
{
	using System.ComponentModel.DataAnnotations;


	public class RegisterUserRequest
	{
		[Required]
		[MaxLength(64)]
		[MinLength(3)]
		public string UserName { get; set; }

		[Required]
		[MinLength(8)]
		public string Password { get; set; }

		[Required]
		[MinLength(8)]
		public string PasswordConfirmation { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}