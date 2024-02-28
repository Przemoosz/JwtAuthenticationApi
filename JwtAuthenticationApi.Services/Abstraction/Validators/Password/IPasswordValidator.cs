namespace JwtAuthenticationApi.Services.Abstraction.Validators.Password;

public interface IPasswordValidator
{
    public bool Validate(string password, string passwordConfirmation);
}