namespace JwtAuthenticationApi.Common.Factories
{
    using JwtAuthenticationApi.Common.Abstraction.Factories;

    /// <summary>
    /// <see cref="Guid"/> wrapper.
    /// </summary>
    internal class GuidFactory : IGuidFactory
    {
        /// <inheritdoc cref="Guid.NewGuid"/>
        public Guid CreateGuid()
        {
            return Guid.NewGuid();
        }
    }
}