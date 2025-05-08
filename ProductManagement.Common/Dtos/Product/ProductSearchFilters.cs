using ProductManagement.Common.Base.WebAPI;

namespace ProductManagement.Common.Dtos.Product
{
	public class ProductSearchFilters : BaseListFilters
	{
		public string? FilterName { get; set; }
		public decimal? FilterMinPrice { get; set; }
		public decimal? FilterMaxPrice { get; set; }

		public ProductSearchFilters() : base()
		{
		}
	}
}
