using Store.data.Model;

using AutoMapper;
using Store.data.dto;

namespace Store.data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Products, supplierdto>().ReverseMap();
            CreateMap<ProductCategories, ProductCategorydto>().ReverseMap();
            CreateMap<Customer, Customerdto>().ReverseMap();
            CreateMap<Suppliers, Suppliersdto>().ReverseMap();
        }
    }
}
