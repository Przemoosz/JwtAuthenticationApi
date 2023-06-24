namespace JwtAuthenticationApi.Abstraction.RuleEngine;

public interface IRuleEngine<TContext>
{
	void Validate(TContext context, IEnumerable<IRule<TContext>> rules);
}