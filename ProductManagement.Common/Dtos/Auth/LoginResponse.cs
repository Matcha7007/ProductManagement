using ProductManagement.Common.Base.WebAPI;

namespace ProductManagement.Common.Dtos.Auth
{
    public class LoginResponse : BaseResponse
	{
		public string BearerToken { get; set; } = string.Empty;
		public string UserFullName { get; set; } = string.Empty;
	}
}
