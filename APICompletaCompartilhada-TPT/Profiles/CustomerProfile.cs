using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Features.Customers.Commands.CreateCustomer;
using Univali.Api.Features.Customers.Commands.CreateCustomerWithAddress;
using Univali.Api.Features.Customers.Commands.PartiallyUpdateCustomer;
using Univali.Api.Features.Customers.Commands.UpdateCustomer;
using Univali.Api.Features.Customers.Commands.UpdateCustomerWithAddress;
using Univali.Api.Features.Customers.Queries.GetCustomerDetail;
using Univali.Api.Models;

namespace Univali.Api.Profiles;

public class CustomerProfile : Profile 
{
    public CustomerProfile() {
  
        CreateMap<Customer, CustomerDto>().ReverseMap();

        CreateMap<CustomerForPatchDto, Customer>().ReverseMap();   

        CreateMap<Customer, GetCustomerDto>();

        CreateMap<CreateCustomerCommand, Customer>();

        CreateMap<Customer, CreateCustomerDto>();

        CreateMap<UpdateCustomerCommand, Customer>();

        CreateMap<Customer, GetCustomersWithAddressesQuery>();

        CreateMap<Customer, GetCustomerWithAddressByIdQuery>();

        //CreateMap<Customer, GetCustomerWithAddressByCpfQuery>();

        CreateMap<UpdateCustomerWithAddressCommand, Customer>();

        CreateMap<CreateCustomerWithAddressDto, Customer>().ReverseMap();

        CreateMap<GetCustomerWithAddressesDto, Customer>().ReverseMap();
        
        CreateMap<CreateCustomerWithAddressCommand, Customer>().ReverseMap();

        CreateMap<Customer, PartiallyUpdateCustomerDto>().ReverseMap();

        // TPT

        CreateMap<CreateCustomerCommand, NaturalCustomer>();

        CreateMap<CreateCustomerCommand, LegalCustomer>();

        CreateMap<LegalCustomer, GetLegalCustomersDetailDto>();
        
        CreateMap<NaturalCustomer, GetNaturalCustomersDetailDto>();

        CreateMap<Customer, GetCustomerDto>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));;

        CreateMap<LegalCustomer, GetLegalCustomersDetailDto>()
            .IncludeBase<Customer, GetCustomerDto>()
            .ForMember(dest => dest.CNPJ, opt => opt.MapFrom(src => src.CNPJ));

        CreateMap<NaturalCustomer, GetNaturalCustomersDetailDto>()
            .IncludeBase<Customer, GetCustomerDto>()
            .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF));


    }
}

