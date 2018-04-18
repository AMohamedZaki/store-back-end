using Store.data.Model;

using AutoMapper;
using Store.data.dto;

namespace Store.data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Products, Productdto>();
            CreateMap<Productdto, Products>();

            CreateMap<ProductCategories, ProductCategorydto>();
            CreateMap<ProductCategorydto, ProductCategories>();

            CreateMap<Customer, Customerdto>();
            CreateMap<Customerdto, Customer>();

        }
    }
}
