using JwtAuthenticationApi.Models.Enums;

namespace JwtAuthenticationApi.Models.Registration.Responses;

/// <summary>
/// User registration response.
/// </summary>
public class RegisterUserResponse
{
    /// <summary>
    /// User unique identifier.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Describes if service execution was successful 
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Describes what kind of error occurred. Null if no errors occurred.
    /// </summary>
    public ErrorType ErrorType { get; set; }

    /// <summary>
    /// Error message. Null if no errors occurred.
    /// </summary>
	public string ErrorMessage { get; set; }
}