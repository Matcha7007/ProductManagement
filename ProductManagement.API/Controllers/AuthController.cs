using MediatR;

using Microsoft.AspNetCore.Mvc;

using ProductManagement.Common.Base.Controller;
using ProductManagement.Common.Dtos.Auth;
using ProductManagement.Common.Base.WebAPI;
using ProductManagement.Features.Auth.Commands;
using System.Text.Json;
using ProductManagement.Common.Extensions;


namespace ProductManagement.API.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class AuthController(IMediator mediator, NLog.ILogger logger) : BaseAPIController
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
				logger.LogInfo(nameof(this.Login), JsonSerializer.Serialize(parameters), response.StatusCode, response.Message, HttpContext?.Request?.Path.ToString()!);
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
				logger.LogException(ex, nameof(this.Login), JsonSerializer.Serialize(parameters), HttpContext?.Request?.Path.ToString()!);
			}
			return ResponseToActionResult(response);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegistrationParameters parameters)
		{
			RegistrationResponse response = new();
			try
			{
				response = await mediator.Send(new RegistrationCommand(parameters));
				logger.LogInfo(nameof(this.Register), JsonSerializer.Serialize(parameters), response.StatusCode, response.Message, HttpContext?.Request?.Path.ToString()!);
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
				logger.LogException(ex, nameof(this.Register), JsonSerializer.Serialize(parameters), HttpContext?.Request?.Path.ToString()!);
			}
			return ResponseToActionResult(response);
		}
	}
}
