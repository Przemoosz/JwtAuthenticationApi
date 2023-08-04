namespace JwtAuthenticationApi.Models.Registration.Requests
{
    using System.ComponentModel.DataAnnotations;
    using Controllers;

    /// <summary>
    /// Register user request. HTTP Request body in <see cref="UserRegisterController"/> is resolved to this class.
    /// </summary>
    public class RegisterUserRequest
    {
        /// <summary>
        /// Username resolved from request.
        /// </summary>
        [Required]
        [MaxLength(64)]
        [MinLength(3)]
        public string Username { get; set; }

        /// <summary>
        /// Password resolved from request.
        /// </summary>
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        /// <summary>
        /// Password confirmation resolved from request.
        /// </summary>
        [Required]
        [MinLength(8)]
        public string PasswordConfirmation { get; set; }

        /// <summary>
        /// Email resolved from request.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}