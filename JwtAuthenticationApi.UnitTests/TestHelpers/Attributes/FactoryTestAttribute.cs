namespace JwtAuthenticationApi.UnitTests.TestHelpers.Attributes
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
	internal sealed class FactoryTestAttribute: CategoryAttribute
	{
	}
}
