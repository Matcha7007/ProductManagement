using AutoMapper;
using MediatR;

using Microsoft.EntityFrameworkCore;

using ProductManagement.Common.Base.WebAPI;
using ProductManagement.Common.Dtos.Product;
using ProductManagement.Common.Extensions;
using ProductManagement.Domain;
using ProductManagement.Persistence;

using System.Text.Json;

namespace ProductManagement.Features.Product.Queries
{
    class SearchProductHandler(ProductManagementDbContext context, IMapper mapper, NLog.ILogger logger) : IRequestHandler<SearchProductQuery, ProductSearchResponse?>
	{
		public async Task<ProductSearchResponse?> Handle(SearchProductQuery request, CancellationToken cancellationToken)
		{
			ProductSearchResponse response = new();
			try
			{
				IQueryable<MstProduct> query = context.MstProducts.AsNoTracking();

				#region Filtering
				var filter = request.Parameters.Filter;

				if (!string.IsNullOrWhiteSpace(filter.FilterName))
				{
					var name = filter.FilterName.Trim().ToLower();
					query = query.Where(x => x.Name.ToLower().Trim().Contains(name));
				}

				if (filter.FilterMinPrice.HasValue || filter.FilterMaxPrice.HasValue)
				{
					if (filter.FilterMinPrice.HasValue)
						query = query.Where(x => x.Price >= filter.FilterMinPrice.Value);

					if (filter.FilterMaxPrice.HasValue)
						query = query.Where(x => x.Price <= filter.FilterMaxPrice.Value);
				}
				#endregion

				#region Sorting Paging
				BaseFuncListResult<MstProduct> listData = await query.ToListWithSortingAndPagingAsync(request!.Parameters);
				response.Data.TotalRecords = listData.TotalRecords;
				response.Data.ListData = mapper.Map<List<ProductDto>>(listData.ListData);
				response.Data.FillCurrentPageAndPageSize(request.Parameters).CalculatePages();
				response.Message = "Search Product";
				#endregion
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
				logger.LogException(ex, nameof(this.Handle), JsonSerializer.Serialize(request.Parameters));
			}
			return response;
		}

	}
}
