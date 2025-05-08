using MediatR;

using Microsoft.AspNetCore.Mvc;

using ProductManagement.Common.Dtos.Auth;

namespace ProductManagement.Features.Auth.Queries
{
	public record ClaimTokenQuery(ControllerBase ControllerBase) : IRequest<ClaimTokenResponse>;
}
