using ProductManagement.Common.Extensions;

using System.Security.Claims;

namespace ProductManagement.Common.Helpers
{
	public static class BearerTokenHelper
	{
		public static string GenerateBearerToken(int userId)
		{
			List<Claim> listClaim = [];
			listClaim.Add(new Claim("UserId", userId.ToString()));

			return JWTTokenHelper.GenerateJWTToken(
				"Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs03Hdx"
				, "JWTAuthenticationSimpleEWallet"
				, "JWTServiceSimpleEWalletClient"
				, 5 > 0 ? DateTime.Now.AddMinutes(5) : null
				, listClaim
			);
		}

		public static bool ValidateBearerToken(string token)
		{
			return JWTTokenHelper.ValidateJWTToken(
				"Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs03Hdx"
				, "JWTAuthenticationSimpleEWallet"
				, "JWTServiceSimpleEWalletClient"
				, token);
		}

		public static int? GetUserId(string token)
		{
			string strUsedId = JWTTokenHelper.GetJWTClaimData(token, "UserId");
			int? result = int.TryParse(strUsedId, out int parseResult)
				? parseResult
				: throw new FormatException("Error when getting user id from token.");
			return result;
		}

		public static string GetTokenValue(string fullBearerToken)
		{
			string token = string.Empty;
			if (fullBearerToken.IsNotNullOrWhiteSpace())
			{
				if (fullBearerToken.StartsWith("BEARER ", StringComparison.OrdinalIgnoreCase))
				{
					token = fullBearerToken.Substring(7);
				}
			}
			return token;
		}
	}
}