using MediatR;

using ProductManagement.Common.Dtos;

namespace ProductManagement.Features.Config.Queries
{
	public record GetConfigByKeyQuery(string ConfigKey) : IRequest<ConfigDto>;
}
