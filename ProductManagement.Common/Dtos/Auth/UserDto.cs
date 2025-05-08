using ProductManagement.Common.Base.WebAPI;

namespace ProductManagement.Common.Dtos.Auth
{
    public class UserDto : BaseDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public bool? IsActive { get; set; }
    }
}

