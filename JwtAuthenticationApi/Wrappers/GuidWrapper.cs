namespace JwtAuthenticationApi.Wrappers
{
	/// <summary>
	/// <see cref="Guid"/> wrapper.
	/// </summary>
	public class GuidWrapper: IGuidWrapper
	{
		public Guid CreateGuid()
		{
			return Guid.NewGuid();
		}
	}
}