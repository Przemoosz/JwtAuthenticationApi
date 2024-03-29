﻿using JwtAuthenticationApi.Models.Enums;
using JwtAuthenticationApi.Models.Registration.Requests;
using JwtAuthenticationApi.Registration;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthenticationApi.Controllers;

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
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]


	public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserRequest registerUserRequest, CancellationToken cancellationToken)
	{
		var result = await _userRegisterService.RegisterUserAsync(registerUserRequest, cancellationToken);
		if (result.IsSuccessful)
		{
			return Created(this.Request?.Path ?? "", result.UserId);
		}

		switch (result.ErrorType)
		{
			case ErrorType.PasswordValidationError:
				return BadRequest(result.ErrorMessage);
			case ErrorType.DbErrorEntityExists:
				return Conflict(result.ErrorMessage);
			case ErrorType.DbError:
			case ErrorType.InternalError:
			default:
				return StatusCode(500, result.ErrorMessage);
		}
	}

}