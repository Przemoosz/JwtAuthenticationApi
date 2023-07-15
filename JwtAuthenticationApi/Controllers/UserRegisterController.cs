using JwtAuthenticationApi.Registration;

namespace JwtAuthenticationApi.Controllers
{
    using Models.Requests;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
	[Route("Register")]
	public class UserRegisterController: ControllerBase
	{
		private readonly IUserRegisterService _userRegisterService;

		public UserRegisterController(IUserRegisterService userRegisterService)
		{
			_userRegisterService = userRegisterService;
		}

		[HttpPost]
		[Route("User")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]


		public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserRequest registerUserRequest, CancellationToken cancellationToken)
		{
			var result = await _userRegisterService.RegisterUserAsync(registerUserRequest, cancellationToken);
			if (result.IsSuccessful)
			{
				return Created(this.Request.Path, result.UserId);
			}

			switch (result.ErrorType)
			{
				case ErrorType.PasswordValidationError:
					return BadRequest(result.ErrorMessage);
				case ErrorType.DbError:
				case ErrorType.InternalError:
					return StatusCode(500, result.ErrorMessage);
			}
			return BadRequest("Wrong dude");
		}

	}
}
