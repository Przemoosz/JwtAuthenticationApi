using JwtAuthenticationApi.Authentication.Registration;

namespace JwtAuthenticationApi.Controllers
{
	using Models.Requests;
	using Microsoft.AspNetCore.Mvc;

	[ApiController]
	[Route("UserAuthentication")]
	public class UserAuthenticationController: ControllerBase
	{
		private readonly IUserRegisterService _userRegisterService;

		public UserAuthenticationController(IUserRegisterService userRegisterService)
		{
			_userRegisterService = userRegisterService;
		}

		[HttpPost]
		[Route("Register")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]

		public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest registerUserRequest, CancellationToken cancellationToken)
		{
			var result = await _userRegisterService.RegisterUserAsync(registerUserRequest, cancellationToken);
			if (result.IsSuccessful)
			{
				return Created("", registerUserRequest);
			}
			return BadRequest("Wrong dude");
		}

	}
}
