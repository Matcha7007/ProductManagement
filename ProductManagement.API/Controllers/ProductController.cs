using MediatR;

using Microsoft.AspNetCore.Mvc;

using ProductManagement.Common.Base.Controller;
using ProductManagement.Common.Base.WebAPI;
using ProductManagement.Common.Dtos.Auth;
using ProductManagement.Common.Dtos.Product;
using ProductManagement.Features.Auth.Queries;
using ProductManagement.Features.Product.Commands;
using ProductManagement.Features.Product.Queries;


namespace ProductManagement.API.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class ProductController(IMediator mediator) : BaseAPIController
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
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
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
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
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
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
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
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
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
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return ResponseToActionResult(response);
		}
	}
}
