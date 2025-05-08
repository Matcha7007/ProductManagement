using ProductManagement.Common.Base.WebAPI;

namespace ProductManagement.Common.Dtos.Product
{
    public class ProductByIdParameters : BaseMethodParameters
    {
        public int Id { get; set; }
	}
}
