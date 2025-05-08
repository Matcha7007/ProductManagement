using ProductManagement.Common.Base.WebAPI;

namespace ProductManagement.Common.Dtos.Product
{
    public class ProductDto : BaseDto
	{
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }
    }
}

