using MediatR;

using Microsoft.EntityFrameworkCore;

using ProductManagement.Common.Dtos.Product;
using ProductManagement.Common.Base.WebAPI;
using ProductManagement.Domain;
using ProductManagement.Persistence;

namespace ProductManagement.Features.Product.Commands
{
    public class CreateProductHandler(ProductManagementDbContext context) : IRequestHandler<CreateProductCommand, ProductResponse?>
    {
        public async Task<ProductResponse?> Handle(CreateProductCommand request, CancellationToken cancellationToken) 
        {
            ProductResponse response = new();
			try
			{
				await context.Database.BeginTransactionAsync(cancellationToken);

				#region Validation
				if (request.Parameters == null)
				{
					response.SetValidationMessage("Parameters is null");
					return response;
				}
				if (string.IsNullOrEmpty(request.Parameters.Name))
				{
					response.SetValidationMessage("Product Name is null or empty");
					return response;
				}
				if (!request.Parameters.Price.HasValue)
				{
					response.SetValidationMessage("Price is null or empty");
					return response;
				}
				#endregion

				#region Check Product
				MstProduct? product = await context.MstProducts
					.FirstOrDefaultAsync(x => x.Name.ToLower() == request.Parameters.Name.ToLower(), cancellationToken);
				if (product != null)
				{
					response.SetValidationMessage($"Product with Name {request.Parameters.Name} already exists");
					return response;
				}
				#endregion

				#region Create Product
				MstProduct newProduct = new()
				{
					Name = request.Parameters.Name,
					Description = request.Parameters.Description,
					Price = (decimal)request.Parameters.Price,
					CreatedAt = DateTime.Now,
					CreatedBy = request.ByUserId,
					LastModifiedAt = DateTime.Now,
					LastModifiedBy = request.ByUserId
				};
				context.MstProducts.Add(newProduct);
				await context.SaveChangesAsync(cancellationToken);
				response.Message = "Product has been created";
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
