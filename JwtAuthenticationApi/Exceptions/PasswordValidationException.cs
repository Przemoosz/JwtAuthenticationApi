namespace JwtAuthenticationApi.Exceptions;

public class PasswordValidationException : Exception
{
    public PasswordValidationException() : base() { }

    public PasswordValidationException(string message) : base(message) { }

    public PasswordValidationException(string message, Exception innerException) : base(message, innerException) { }
}