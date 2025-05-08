using ProductManagement.Common.Base.WebAPI;

namespace ProductManagement.Common.Dtos.Product
{
    public class ProductParameters : BaseMethodParameters
	{
		public int? Id { get; set; }

		public string? Name { get; set; } = string.Empty;

		public string? Description { get; set; } = string.Empty;

		public decimal? Price { get; set; }
	}
}
