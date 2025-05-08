using MediatR;

using Microsoft.EntityFrameworkCore;

using ProductManagement.Common.Dtos.Product;
using ProductManagement.Common.Base.WebAPI;
using ProductManagement.Domain;
using ProductManagement.Persistence;

namespace ProductManagement.Features.Product.Commands
{
	public class DeleteProductHandler(ProductManagementDbContext context) : IRequestHandler<DeleteProductCommand, ProductResponse?>
	{
		public async Task<ProductResponse?> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
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

				#region Delete Product

				context.MstProducts.Remove(product);
				await context.SaveChangesAsync(cancellationToken);
				response.Message = "Product has been deleted";
				#endregion

				await context.Database.CommitTransactionAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
				await context.Database.RollbackTransactionAsync(cancellationToken);
			}
			return response;
		}
	}
}
