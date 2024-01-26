namespace JwtAuthenticationApi.Commands
{
	using Common.Abstraction.Commands;
	using Common.Models;
	using Infrastructure.Entities;
	using Services.Models.Registration.Requests;

	/// <summary>
    /// Command that is responsible for creating <see cref="UserEntity"/> from request. Implements <see cref="ICommand{TResult}"/>.
    /// </summary>
    public class ConvertRequestToUserEntityCommand : ICommand<UserEntity>
    {
        private readonly RegisterUserRequest _registerUserRequest;
        private readonly string _hashedPassword;

        /// <summary>
        /// Initializes a new <see cref="ConvertRequestToUserEntityCommand"/> with user request and hashed password.
        /// </summary>
		/// <param name="registerUserRequest">User registration request.</param>
		/// <param name="hashedPassword">Hashed user password using SHA256 hashing algorithm.</param>
		public ConvertRequestToUserEntityCommand(RegisterUserRequest registerUserRequest, string hashedPassword)
        {
            _registerUserRequest = registerUserRequest;
            _hashedPassword = hashedPassword;
        }

		/// <summary>
		/// Executes creating user entity from request.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>A task that represents the asynchronous creating of user entity operation.
		/// The task result contains <see cref="Result{TResult}"/> object that contains user entity.</returns>
		public Task<Result<UserEntity>> ExecuteAsync(CancellationToken cancellationToken)
        {
            UserEntity userModel = new UserEntity(_registerUserRequest.Username, _hashedPassword, _registerUserRequest.Email);
            Result<UserEntity> result = new Result<UserEntity>(userModel, true);
            return Task.FromResult(result);
        }
    }
}
