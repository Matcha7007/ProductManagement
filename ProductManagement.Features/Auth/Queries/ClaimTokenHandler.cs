using MediatR;
using ProductManagement.Common.Dtos.Auth;
using ProductManagement.Common.Base.WebAPI;
using ProductManagement.Common.Helpers;

namespace ProductManagement.Features.Auth.Queries
{
	public class ClaimTokenHandler : IRequestHandler<ClaimTokenQuery, ClaimTokenResponse?>
	{
		public Task<ClaimTokenResponse?> Handle(ClaimTokenQuery request, CancellationToken cancellationToken)
		{
			ClaimTokenResponse response = new();
			try
			{
				string? token = HttpHelper.ExtractBearerToken(request.ControllerBase);
				if (string.IsNullOrEmpty(token))
				{
					return Task.FromResult<ClaimTokenResponse?>(response.SetUnauthorized());
				}
				int? userId = BearerTokenHelper.GetUserId(token!);
				bool isValid = BearerTokenHelper.ValidateBearerToken(token!);
				if (userId == null || !isValid)
				{					
					return Task.FromResult<ClaimTokenResponse?>(response.SetUnauthorized());
				}
				response.Data = new()
				{
					UserId = (int)userId,
					Token = token!,
					IsTokenValid = isValid
				};
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return Task.FromResult<ClaimTokenResponse?>(response);
		}
	}
}
