using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Features.Addresses.Commands.CreateAddress;
using Univali.Api.Features.Addresses.Commands.UpdateAddresses;
using Univali.Api.Features.Addresses.Queries.GetCustomerDetail;
using Univali.Api.Models;

namespace Univali.Api.Profiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressDto>().ReverseMap();

        CreateMap<Address, GetAddressDetailDto>();

        CreateMap<CreateAddressCommand, Address>();

        CreateMap<Address, CreateAddressDto>();
        
        CreateMap<UpdateAddressCommand, Address>();
    }
}