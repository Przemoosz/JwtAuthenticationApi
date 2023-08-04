namespace JwtAuthenticationApi.Extensions
{
	using Abstraction.RuleEngine;

	/// <summary>
	/// Extension methods for List to create list of rules.
	/// </summary>
	public static class ListExtension
	{
		/// <summary>
		/// Adds rules to <see cref="rulesList"/> using params.
		/// </summary>
		/// <typeparam name="TContext">Type of context in password rules.</typeparam>
		/// <param name="rulesList">List of rules.</param>
		/// <param name="rules">Rules to add.</param>
		public static void AddRules<TContext>(this List<IRule<TContext>> rulesList, params IRule<TContext>[] rules) 
			where TContext : class
		{
			rulesList.AddRange(rules);
		}
	}
}
