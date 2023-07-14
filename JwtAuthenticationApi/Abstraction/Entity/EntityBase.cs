namespace JwtAuthenticationApi.Abstraction.Entity
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // generate for insert new only
        public int Id { get; init; }
    }
}