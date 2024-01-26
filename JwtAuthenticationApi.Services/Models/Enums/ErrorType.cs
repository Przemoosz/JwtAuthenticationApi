namespace JwtAuthenticationApi.Services.Models.Enums;

/// <summary>
/// Error types that can occurs in services.
/// </summary>
public enum ErrorType
{
    DbError,
    PasswordValidationError,
    InternalError,
    DbErrorEntityExists
}