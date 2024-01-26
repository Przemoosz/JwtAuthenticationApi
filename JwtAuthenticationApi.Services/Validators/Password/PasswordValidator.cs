namespace JwtAuthenticationApi.Services.Validators.Password
{
	using JwtAuthenticationApi.Services.Abstraction.Factories.Password;
	using Exceptions;
	using JwtAuthenticationApi.Services.Models.Password;
	using Abstraction.RuleEngine;
	using JwtAuthenticationApi.Services.Abstraction.Validators.Password;
	using Extensions;
	using ILogger = Serilog.ILogger;

	internal sealed class PasswordValidator: IPasswordValidator
    {
	    private const int TotalRules = 5;
	    private readonly IPasswordContextFactory _passwordContextFactory;
	    private readonly IRuleEngine<PasswordContext> _passwordRuleEngine;
	    private readonly IPasswordRuleFactory _passwordRuleFactory;
	    private readonly ILogger _logger;


	    public PasswordValidator(IPasswordContextFactory passwordContextFactory,
		    IRuleEngine<PasswordContext> passwordRuleEngine, IPasswordRuleFactory passwordRuleFactory,
		    ILogger logger)
	    {
		    _passwordContextFactory = passwordContextFactory;
		    _passwordRuleEngine = passwordRuleEngine;
		    _passwordRuleFactory = passwordRuleFactory;
		    _logger = logger;
	    }

        public bool Validate(string password, string passwordConfirmation)
        {
	        var context = _passwordContextFactory.Create(password, passwordConfirmation);
			List<IRule<PasswordContext>> rules = new List<IRule<PasswordContext>>(TotalRules);
			rules.AddRules(
				_passwordRuleFactory.CreateEqualityRule(),
				_passwordRuleFactory.CreateLengthRule(),
				_passwordRuleFactory.CreateLowerLettersRule(),
				_passwordRuleFactory.CreateUpperLettersRule(),
				_passwordRuleFactory.CreateSpecialLetterRule());
			try
			{
				_passwordRuleEngine.Validate(context, rules);
			}
			catch (PasswordValidationException exception)
			{
				_logger.Error($"Password validation error occurred - {exception.Message}");
				return false;
			}
			return true;
        }
    }
}