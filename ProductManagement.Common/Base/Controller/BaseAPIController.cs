using System.Net;

using Microsoft.AspNetCore.Mvc;

using ProductManagement.Common.Base.WebAPI;

namespace ProductManagement.Common.Base.Controller
{
	/// <summary>
	/// Provides a base class for an API Controller
	/// </summary>
	[ApiController]
	[Route("api/v1/[controller]")]
	public class BaseAPIController : ControllerBase
	{
		/// <summary>
		/// Provides a default controller.
		/// </summary>
		public BaseAPIController()
		{
		}

		/// <summary>
		/// Check whether the controller is online or not.
		/// </summary>
		/// <returns>A string</returns>
		[NonAction]
		public string IsOnlineMessage() => GetType().Name + " is online at " + DateTime.Now.ToString("dd MMM yyyy HH:mm:ss.ffff");

		/// <summary>
		/// This function for return response
		/// </summary>
		/// <typeparam name="TResponse"></typeparam>
		/// <param name="response"></param>
		/// <returns></returns>
		protected IActionResult ResponseToActionResult<TResponse>(TResponse response) where TResponse : BaseResponse, new()
		{
			switch ((HttpStatusCode)response.StatusCode)
			{
				case HttpStatusCode.OK:
					return Ok(response);
				case HttpStatusCode.Unauthorized:
					if (string.IsNullOrWhiteSpace(response.Message))
					{
						response.Message = "You are not authorized.";
					}

					return Unauthorized();
				case HttpStatusCode.NotFound:
					if (string.IsNullOrWhiteSpace(response.Message))
					{
						response.Message = "Not found.";
					}

					return NotFound(response);
				default:
					return StatusCode(response.StatusCode, response);
			}
		}
	}
}
