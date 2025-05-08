using MediatR;

namespace ProductManagement.Features.Config.Queries
{
	public record GetConfigByKeyValueQuery(string ConfigKey) : IRequest<string>;
}
