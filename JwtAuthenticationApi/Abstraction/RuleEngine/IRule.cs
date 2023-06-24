namespace JwtAuthenticationApi.Abstraction.RuleEngine
{
    public interface IRule<in TContext>
	{
		void Evaluate(TContext context);
		bool CanEvaluateRule(TContext context);
	}
}
