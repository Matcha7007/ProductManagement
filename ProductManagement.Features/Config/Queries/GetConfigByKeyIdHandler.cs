using MediatR;

using Microsoft.EntityFrameworkCore;

using ProductManagement.Persistence;

namespace ProductManagement.Features.Config.Queries
{
	public class GetConfigByKeyIdHandler(ProductManagementDbContext context) : IRequestHandler<GetConfigByKeyIdQuery, int>
	{
		public async Task<int> Handle(GetConfigByKeyIdQuery request, CancellationToken cancellationToken)
		{
			int config = await context.MstConfigs
				.Where(x => x.ConfigKey.ToLower() == request.ConfigKey.ToLower() && x.IsActive == true)
				.Select(x => int.Parse(x.ConfigValue!))
				.SingleOrDefaultAsync(cancellationToken: cancellationToken);

			return config;
		}
	}
}
