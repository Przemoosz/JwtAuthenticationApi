namespace JwtAuthenticationApi.Controllers
{
	using Models.Requests;
	using Microsoft.AspNetCore.Mvc;

	[ApiController]
	[Route("UserAuthentication")]
	public class UserAuthenticationController: ControllerBase
	{

		[HttpPost]
		[Route("Register")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]

		public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest registerUserRequest)
		{
			return Created("", registerUserRequest);
		}

	}
}
