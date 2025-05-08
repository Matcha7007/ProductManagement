using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using ProductManagement.Common.Dtos.Product;
using ProductManagement.Common.Base.WebAPI;
using ProductManagement.Domain;
using ProductManagement.Persistence;
using System.Text.Json;
using ProductManagement.Common.Extensions;

namespace ProductManagement.Features.Product.Queries
{
    public class GetProductByIdHandler(ProductManagementDbContext context, IMapper mapper, NLog.ILogger logger) : IRequestHandler<GetProductByIdQuery, ProductByIdResponse?>
	{
		public async Task<ProductByIdResponse?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
		{
			ProductByIdResponse response = new();
			try
			{
				#region Validation
				if (request.Parameters == null || request.Parameters.Id == 0)
				{
					response.SetValidationMessage("Parameters Id is null");
					return response;
				}
				#endregion

				#region Check Product
				MstProduct? product = await context.MstProducts
					.FirstOrDefaultAsync(x => x.Id == request.Parameters.Id, cancellationToken);
				if (product == null)
				{
					response.SetValidationMessage($"Product with Id {request.Parameters.Id} is not exists");
					response.StatusCode = 404;
					return response;
				}
				#endregion

				#region Mapping Product
				response.Data = mapper.Map<ProductDto>(product);
				response.Message = "Get Product Data by Id";
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
