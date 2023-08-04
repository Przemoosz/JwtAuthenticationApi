namespace JwtAuthenticationApi.Abstraction.Models
{
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// Base class for all models in relational databases.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public abstract class ModelBase
	{
		/// <summary>
		/// Entity unique identifier.
		/// </summary>
		public int Id { get; init; }

		protected ModelBase(int id)
		{
			Id = id;
		}
	}
}