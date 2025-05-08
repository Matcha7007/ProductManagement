using MediatR;

namespace ProductManagement.Features.Config.Queries
{
	public record GetConfigByKeyIdQuery(string ConfigKey) : IRequest<int>;
}
