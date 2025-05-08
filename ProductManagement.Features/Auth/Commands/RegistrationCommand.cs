using MediatR;

using ProductManagement.Common.Dtos.Auth;

namespace ProductManagement.Features.Auth.Commands
{
	public record RegistrationCommand(RegistrationParameters Parameters) : IRequest<RegistrationResponse>;
}
