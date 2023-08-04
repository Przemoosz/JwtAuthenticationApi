namespace JwtAuthenticationApi.Abstraction.RuleEngine
{
	/// <summary>
	/// Defines methods for rule in rule engine.
	/// </summary>
	/// <typeparam name="TContext">Type of rule context.</typeparam>
    public interface IRule<in TContext>
	{
		/// <summary>
		/// Evaluate rule.
		/// </summary>
		/// <param name="context">Context for rule.</param>
		void Evaluate(TContext context);
		/// <summary>
		/// Provides information whether rule evaluation is allowed.
		/// </summary>
		/// <param name="context">Context for rule.</param>
		/// <returns><see langword="true"/> or <see langword="false"/> if base evaluation is allowed.</returns>
		bool CanEvaluateRule(TContext context);
	}
}
