using ProductManagement.Common.Base.WebAPI;

namespace ProductManagement.Common.Dtos.Auth
{
	public class ClaimTokenDto : BaseDto
	{
		public int UserId { get; set; }
		public string Token { get; set; } = string.Empty;
		public bool IsTokenValid { get; set; }
	}
}

