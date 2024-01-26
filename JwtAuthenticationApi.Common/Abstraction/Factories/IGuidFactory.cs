namespace JwtAuthenticationApi.Common.Abstraction.Factories
{
    /// <summary>
    /// Defines method for <see cref="Guid"/> wrapper.
    /// </summary>
    public interface IGuidFactory
    {
        /// <inheritdoc cref="Guid.NewGuid"/>
        Guid CreateGuid();
    }
}