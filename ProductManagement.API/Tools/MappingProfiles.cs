using AutoMapper;

using ProductManagement.Common.Dtos.Product;
using ProductManagement.Domain;


namespace ProductManagement.API.Tools
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<MstProduct, ProductDto>().ReverseMap();
		}
	}
}
