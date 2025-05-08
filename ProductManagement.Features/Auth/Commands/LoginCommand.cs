using MediatR;

using ProductManagement.Common.Dtos.Auth;

namespace ProductManagement.Features.Auth.Commands
{
	public record LoginCommand(LoginParameters Parameters) : IRequest<LoginResponse>;
}
