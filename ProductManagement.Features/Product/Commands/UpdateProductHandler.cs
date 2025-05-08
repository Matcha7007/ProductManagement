using MediatR;
using Microsoft.EntityFrameworkCore;

using ProductManagement.Common.Dtos.Product;
using ProductManagement.Common.Base.WebAPI;
using ProductManagement.Domain;
using ProductManagement.Persistence;
using System.Text.Json;
using ProductManagement.Common.Extensions;

namespace ProductManagement.Features.Product.Commands
{
    public class UpdateProductHandler(ProductManagementDbContext context, NLog.ILogger logger) : IRequestHandler<UpdateProductCommand, ProductResponse?>
	{
		public async Task<ProductResponse?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
		{
			ProductResponse response = new();
			try
			{
				await context.Database.BeginTransactionAsync(cancellationToken);

				#region Validation
				if (request.Parameters.Id == null)
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

				#region Update Product
				if (!string.IsNullOrEmpty(request.Parameters.Name))
					product.Name = request.Parameters.Name;
				if (!string.IsNullOrEmpty(request.Parameters.Description))
					product.Description = request.Parameters.Description;
				if (request.Parameters.Price.HasValue)
					product.Price = request.Parameters.Price.Value;

				product.LastModifiedAt = DateTime.Now;
				product.LastModifiedBy = request.ByUserId;

				await context.SaveChangesAsync(cancellationToken);
				response.Message = "Product has been updated";
				#endregion

				await context.Database.CommitTransactionAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
				await context.Database.RollbackTransactionAsync(cancellationToken);
				logger.LogException(ex, nameof(this.Handle), JsonSerializer.Serialize(request.Parameters));
			}
			return response;
		}

	}
}
