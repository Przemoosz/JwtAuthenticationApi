namespace JwtAuthenticationApi.Factories.Password
{
	using Abstraction.RuleEngine;
	using Models.Password;
	using Validators.Password.Rules;

	/// <summary>
	/// Defines methods for password rules factory.
	/// </summary>
	public interface IPasswordRuleFactory
	{
		/// <summary>
		/// Creates and returns <see cref="EqualityRule"/>.
		/// </summary>
		/// <returns>Instance of <see cref="EqualityRule"/> that implements <see cref="IRule{TContext}"/></returns>
		IRule<PasswordContext> CreateEqualityRule();

		/// <summary>
		/// Creates and returns <see cref="LengthRule"/>.
		/// </summary>
		/// <returns>Instance of <see cref="LengthRule"/> that implements <see cref="IRule{TContext}"/></returns>
		IRule<PasswordContext> CreateLengthRule();

		/// <summary>
		/// Creates and returns <see cref="LowerLettersRule"/>.
		/// </summary>
		/// <returns>Instance of <see cref="LowerLettersRule"/> that implements <see cref="IRule{TContext}"/></returns>
		IRule<PasswordContext> CreateLowerLettersRule();

		/// <summary>
		/// Creates and returns <see cref="SpecialLettersRule"/>.
		/// </summary>
		/// <returns>Instance of <see cref="SpecialLettersRule"/> that implements <see cref="IRule{TContext}"/></returns>
		IRule<PasswordContext> CreateSpecialLetterRule();

		/// <summary>
		/// Creates and returns <see cref="UpperLettersRule"/>.
		/// </summary>
		/// <returns>Instance of <see cref="UpperLettersRule"/> that implements <see cref="IRule{TContext}"/></returns>
		IRule<PasswordContext> CreateUpperLettersRule();
	}
}