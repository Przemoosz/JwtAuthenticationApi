namespace JwtAuthenticationApi.Extensions
{
	using Abstraction.RuleEngine;

	public static class RulesExtension
	{
		public static void AddRules<TContext>(this List<IRule<TContext>> rulesList, params IRule<TContext>[] rules) 
			where TContext : class
		{
			rulesList.AddRange(rules);
		}
	}
}
