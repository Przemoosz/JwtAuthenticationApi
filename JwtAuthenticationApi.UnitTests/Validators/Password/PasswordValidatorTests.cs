using JwtAuthenticationApi.Abstraction.RuleEngine;
using JwtAuthenticationApi.Factories.Password;
using JwtAuthenticationApi.Models.Password;
using JwtAuthenticationApi.Validators.Password;

namespace JwtAuthenticationApi.UnitTests.Validators.Password
{
	[TestFixture, Parallelizable]
	public sealed class PasswordValidatorTests
	{

		[SetUp]
		public void SetUp()
		{
			_passwordContextFactory = Substitute.For<IPasswordContextFactory>();
			_passwordRuleFactory = Substitute.For<IPasswordRuleFactory>();
			_passwordRuleEngine = Substitute.For<IRuleEngine<PasswordContext>>();
			_sut = new PasswordValidator()
		}
	}
}
