namespace JwtAuthenticationApi.Wrappers
{
	public class GuidWrapper: IGuidWrapper
	{
		public Guid CreateGuid()
		{
			return Guid.NewGuid();
		}
	}
}