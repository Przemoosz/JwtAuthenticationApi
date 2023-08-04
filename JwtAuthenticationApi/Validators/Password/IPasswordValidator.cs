namespace JwtAuthenticationApi.Validators.Password;

public interface IPasswordValidator
{
	public bool Validate(string password, string passwordConfirmation);
}