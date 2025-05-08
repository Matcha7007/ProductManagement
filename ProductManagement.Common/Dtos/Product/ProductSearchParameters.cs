using ProductManagement.Common.Base.WebAPI;

namespace ProductManagement.Common.Dtos.Product
{
    public class ProductSearchParameters : BaseListParameters<ProductSearchFilters>
	{
		public ProductSearchParameters() : base() { }
	}
}
