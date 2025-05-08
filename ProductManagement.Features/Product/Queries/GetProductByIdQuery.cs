using MediatR;

using ProductManagement.Common.Dtos.Product;

namespace ProductManagement.Features.Product.Queries
{
    public record GetProductByIdQuery(ProductByIdParameters Parameters) : IRequest<ProductByIdResponse>;
}
