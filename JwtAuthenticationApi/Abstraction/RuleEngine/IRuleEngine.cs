namespace JwtAuthenticationApi.Abstraction.RuleEngine
{

	/// <summary>
	/// Defines method for rule engine.
	/// </summary>
	/// <typeparam name="TContext">Type of context.</typeparam>
	public interface IRuleEngine<TContext>
	{
		/// <summary>
		/// Runs validation of context, based on provided rules.
		/// </summary>
		/// <param name="context">Context that will be validated.</param>
		/// <param name="rules">Rules for validation.</param>
		void Validate(TContext context, IEnumerable<IRule<TContext>> rules);
	}
}