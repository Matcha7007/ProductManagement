using MediatR;

using Microsoft.EntityFrameworkCore;

using ProductManagement.Persistence;

namespace ProductManagement.Features.Config.Queries
{
	public class GetConfigByKeyValueHandler(ProductManagementDbContext context) : IRequestHandler<GetConfigByKeyValueQuery, string?>
	{
		public async Task<string?> Handle(GetConfigByKeyValueQuery request, CancellationToken cancellationToken)
		{
			string? config = await context.MstConfigs
				.Where(x => x.ConfigKey.ToLower() == request.ConfigKey.ToLower() && x.IsActive == true)
				.Select(x => x.ConfigValue)
				.SingleOrDefaultAsync(cancellationToken: cancellationToken);

			return config is null ? null : config;
		}
	}
}
