using ProductManagement.Common.Base.WebAPI;

namespace ProductManagement.Common.Dtos.Auth
{
    public class LoginParameters : BaseMethodParameters
	{
		public string UserName { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
	}
}
