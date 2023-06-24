using JwtAuthenticationApi.Validators.Password.RuleEngine;

namespace JwtAuthenticationApi.Validators.Password
{
    public class PasswordValidator
    {
        public bool Validate(string password, string passwordConfirmation)
        {
            // PasswordContext passwordContext = new PasswordContext(){Password = password, PasswordConfirmation = passwordConfirmation};
            // var chars = passwordContext.Password.ToCharArray();
            int capital = 0;
            int lower = 0;
            int digit = 0;
            return true;
        }
    }
}
