using ProductManagement.Common.Base.WebAPI;

namespace ProductManagement.Common.Dtos.Auth
{
    public class RegistrationParameters : BaseMethodParameters
    {
		public Guid? UserId { get; set; }
		public string? Username { get; set; }
		public string? FullName { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public string? Password { get; set; }
	}
}
