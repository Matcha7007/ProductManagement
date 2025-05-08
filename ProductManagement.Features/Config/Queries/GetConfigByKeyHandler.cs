using MediatR;

using Microsoft.EntityFrameworkCore;

using ProductManagement.Common.Dtos;
using ProductManagement.Domain;
using ProductManagement.Persistence;

namespace ProductManagement.Features.Config.Queries
{
	public class GetConfigByKeyHandler(ProductManagementDbContext context) : IRequestHandler<GetConfigByKeyQuery, ConfigDto?>
	{
		public async Task<ConfigDto?> Handle(GetConfigByKeyQuery request, CancellationToken cancellationToken)
		{
			MstConfig? config = await context.MstConfigs
				.Where(x => x.ConfigKey.ToLower() == request.ConfigKey.ToLower() && x.IsActive == true)
				.SingleOrDefaultAsync(cancellationToken: cancellationToken);

			return config is null ? null : new ConfigDto(config.Id, config.ConfigKey, config.ConfigValue!, config.IsActive == true);
		}
	}
}
