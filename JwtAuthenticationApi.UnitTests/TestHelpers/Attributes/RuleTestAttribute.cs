namespace JwtAuthenticationApi.UnitTests.TestHelpers.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
	public sealed class RuleTestAttribute: CategoryAttribute
	{
	}
}