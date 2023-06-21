namespace JwtAuthenticationApi.Validators.Password.RuleEngine;

public class PasswordContext
{
    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }
}