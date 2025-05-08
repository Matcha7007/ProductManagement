using MediatR;

using ProductManagement.Common.Dtos.Product;

namespace ProductManagement.Features.Product.Queries
{
    public record SearchProductQuery(ProductSearchParameters Parameters) : IRequest<ProductSearchResponse>;
}
