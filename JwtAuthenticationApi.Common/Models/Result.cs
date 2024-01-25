namespace JwtAuthenticationApi.Common.Models
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a result of command execution.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    [ExcludeFromCodeCoverage]
    public sealed class Result<TResult>
    {
        /// <summary>
        /// Result value.
        /// </summary>
        public TResult Value { get; init; }

        /// <summary>
        /// Gets whether command execution was successful.
        /// </summary>
        public bool IsSuccessful { get; init; }

        /// <summary>
        /// Initializes new instance of <see cref="Result{TResult}"/>
        /// </summary>
        /// <param name="value">Command execution result.</param>
        /// <param name="isSuccessful">Specifies if command execution was successful.</param>
        public Result(TResult value, bool isSuccessful)
        {
            Value = value;
            IsSuccessful = isSuccessful;
        }
    }
}