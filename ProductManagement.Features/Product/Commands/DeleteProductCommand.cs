using MediatR;

using ProductManagement.Common.Dtos.Product;

namespace ProductManagement.Features.Product.Commands
{
    public record DeleteProductCommand(ProductParameters Parameters) : IRequest<ProductResponse>;
}
