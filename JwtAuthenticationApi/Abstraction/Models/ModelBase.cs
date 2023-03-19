namespace JwtAuthenticationApi.Abstraction.Models
{
	/// <summary>
	/// Base class for all models in relational databases.
	/// </summary>
	public abstract class ModelBase
	{
		/// <summary>
		/// Entity unique identifier.
		/// </summary>
		public Guid Id { get; init; }
	}
}