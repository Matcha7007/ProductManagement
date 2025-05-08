using Microsoft.AspNetCore.Mvc;

using ProductManagement.Common.Base.WebAPI;

namespace ProductManagement.Common.Dtos.Auth
{
    public class ClaimTokenParameters : BaseMethodParameters
	{
		public required ControllerBase ControllerBase { get; set; }
	}
}
