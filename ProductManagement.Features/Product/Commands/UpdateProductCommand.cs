using MediatR;

using ProductManagement.Common.Dtos.Product;

namespace ProductManagement.Features.Product.Commands
{
    public record UpdateProductCommand(ProductParameters Parameters, int ByUserId) : IRequest<ProductResponse>;
}
