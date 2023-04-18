namespace JwtAuthenticationApi.Wrappers
{
	/// <summary>
	/// Defines method for <see cref="Guid"/> wrapper.
	/// </summary>
	public interface IGuidWrapper
	{
		/// <inheritdoc cref="Guid.NewGuid"/>
		Guid CreateGuid();
	}
}