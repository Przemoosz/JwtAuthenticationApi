namespace JwtAuthenticationApi.Models.Options
{
	/// <summary>
	/// Stores password pepper.
	/// </summary>
	public sealed class PasswordPepper
	{
		/// <summary>
		/// Gets password pepper.
		/// </summary>
		/// <value><see cref="string"/> value of password pepper.</value>
		public string Pepper { get; init; }
	}
}
