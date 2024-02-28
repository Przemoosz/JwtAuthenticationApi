namespace JwtAuthenticationApi.Infrastructure.Abstraction.Entity
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	/// <summary>
	/// Represents base property for Entity.
	/// </summary>
	public abstract class EntityBase
    {
        /// <summary>
        /// Entity Identifier. Represented by <see cref="int"/> value.
        /// </summary>
        /// <remakrs>
        /// This value is auto set by database.
        /// </remakrs>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // generate for insert new only
        public int Id { get; init; }
    }
}