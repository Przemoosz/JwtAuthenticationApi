namespace JwtAuthenticationApi.Wrappers
{
	/// <summary>
	/// <see cref="Guid"/> wrapper.
	/// </summary>
	public class GuidWrapper: IGuidWrapper
	{
		/// <inheritdoc cref="Guid.NewGuid"/>
		public Guid CreateGuid()
		{
			return Guid.NewGuid();
		}
	}
}