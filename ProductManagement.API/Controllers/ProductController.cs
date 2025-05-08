using MediatR;

using Microsoft.AspNetCore.Mvc;

using ProductManagement.Common.Base.Controller;
using ProductManagement.Common.Base.WebAPI;
using ProductManagement.Common.Dtos.Auth;
using ProductManagement.Common.Dtos.Product;
using ProductManagement.Common.Extensions;
using ProductManagement.Features.Auth.Queries;
using ProductManagement.Features.Product.Commands;
using ProductManagement.Features.Product.Queries;

using System.Text.Json;


namespace ProductManagement.API.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class ProductController(IMediator mediator, NLog.ILogger logger) : BaseAPIController
	{
		[HttpGet("is-online")]
		public string IsOnline() => IsOnlineMessage();

		[HttpPost("search")]
		public async Task<IActionResult> Search([FromBody] ProductSearchParameters parameters)
		{
			ProductSearchResponse response = new();
			try
			{
				ClaimTokenResponse claimTokenResponse = await mediator.Send(new ClaimTokenQuery(this));
				_ = !claimTokenResponse.IsValid ? response.SetUnauthorized() : response = await mediator.Send(new SearchProductQuery(parameters));
				logger.LogInfo(nameof(this.Search), JsonSerializer.Serialize(parameters), response.StatusCode, response.Message, HttpContext?.Request?.Path.ToString()!);
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
				logger.LogException(ex, nameof(this.Search), JsonSerializer.Serialize(parameters), HttpContext?.Request?.Path.ToString()!);
			}
			return ResponseToActionResult(response);
		}

		[HttpPost("get-by-id")]
		public async Task<IActionResult> GetById([FromBody] ProductByIdParameters parameters)
		{
			ProductByIdResponse response = new();
			try
			{
				ClaimTokenResponse claimTokenResponse = await mediator.Send(new ClaimTokenQuery(this));
				_ = !claimTokenResponse.IsValid ? response.SetUnauthorized() : response = await mediator.Send(new GetProductByIdQuery(parameters));
				logger.LogInfo(nameof(this.GetById), JsonSerializer.Serialize(parameters), response.StatusCode, response.Message, HttpContext?.Request?.Path.ToString()!);
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
				logger.LogException(ex, nameof(this.GetById), JsonSerializer.Serialize(parameters), HttpContext?.Request?.Path.ToString()!);
			}
			return ResponseToActionResult(response);
		}

		[HttpPost("create")]
		public async Task<IActionResult> Create([FromBody] ProductParameters parameters)
		{
			ProductResponse response = new();
			try
			{
				ClaimTokenResponse claimTokenResponse = await mediator.Send(new ClaimTokenQuery(this));
				_ = !claimTokenResponse.IsValid ? response.SetUnauthorized() : response = await mediator.Send(new CreateProductCommand(parameters, claimTokenResponse.Data.UserId));
				logger.LogInfo(nameof(this.Create), JsonSerializer.Serialize(parameters), response.StatusCode, response.Message, HttpContext?.Request?.Path.ToString()!);
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
				logger.LogException(ex, nameof(this.Create), JsonSerializer.Serialize(parameters), HttpContext?.Request?.Path.ToString()!);
			}
			return ResponseToActionResult(response);
		}

		[HttpPost("update")]
		public async Task<IActionResult> Update([FromBody] ProductParameters parameters)
		{
			ProductResponse response = new();
			try
			{
				ClaimTokenResponse claimTokenResponse = await mediator.Send(new ClaimTokenQuery(this));
				_ = !claimTokenResponse.IsValid ? response.SetUnauthorized() : response = await mediator.Send(new UpdateProductCommand(parameters, claimTokenResponse.Data.UserId));
				logger.LogInfo(nameof(this.Update), JsonSerializer.Serialize(parameters), response.StatusCode, response.Message, HttpContext?.Request?.Path.ToString()!);
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
				logger.LogException(ex, nameof(this.Update), JsonSerializer.Serialize(parameters), HttpContext?.Request?.Path.ToString()!);
			}
			return ResponseToActionResult(response);
		}

		[HttpPost("delete")]
		public async Task<IActionResult> Delete([FromBody] ProductParameters parameters)
		{
			ProductResponse response = new();
			try
			{
				ClaimTokenResponse claimTokenResponse = await mediator.Send(new ClaimTokenQuery(this));
				_ = !claimTokenResponse.IsValid ? response.SetUnauthorized() : response = await mediator.Send(new DeleteProductCommand(parameters));
				logger.LogInfo(nameof(this.Delete), JsonSerializer.Serialize(parameters), response.StatusCode, response.Message, HttpContext?.Request?.Path.ToString()!);
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
				logger.LogException(ex, nameof(this.Delete), JsonSerializer.Serialize(parameters), HttpContext?.Request?.Path.ToString()!);
			}
			return ResponseToActionResult(response);
		}
	}
}
