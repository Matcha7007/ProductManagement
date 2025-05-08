using MediatR;

using Microsoft.AspNetCore.Mvc;

using ProductManagement.Common.Base.Controller;
using ProductManagement.Common.Dtos.Auth;
using ProductManagement.Common.Base.WebAPI;
using ProductManagement.Features.Auth.Commands;


namespace ProductManagement.API.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class AuthController(IMediator mediator) : BaseAPIController
	{
		[HttpGet("is-online")]
		public string IsOnline() => IsOnlineMessage();

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginParameters parameters)
		{
			LoginResponse response = new();
			try
			{
				response = await mediator.Send(new LoginCommand(parameters));
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return ResponseToActionResult(response);
		}

		[HttpPost("registration")]
		public async Task<IActionResult> Registration([FromBody] RegistrationParameters parameters)
		{
			RegistrationResponse response = new();
			try
			{
				response = await mediator.Send(new RegistrationCommand(parameters));
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return ResponseToActionResult(response);
		}
	}
}
