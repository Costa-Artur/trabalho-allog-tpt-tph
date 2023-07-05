using AutoMapper;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Addresses.Queries.GetCustomerDetail;

public class GetAddressByIdDetailQueryHandler : 
    IRequestHandler<GetAddressByIdDetailQuery, GetAddressDetailDto>
{
    private readonly ICustomerRepository _customerRepository;

    private readonly IMapper _mapper;

    public GetAddressByIdDetailQueryHandler(
        ICustomerRepository customerRepository, 
        IMapper mapper
    ) {
        _customerRepository = customerRepository;

        _mapper = mapper;
    }

    public async Task<GetAddressDetailDto> Handle(
        GetAddressByIdDetailQuery getAddressByIdDetailQuery, 
        CancellationToken cancellationToken
    ) {
        var customerFromDatabase = await _customerRepository
            .GetAddressAsync(
                getAddressByIdDetailQuery.CustomerId, 
                getAddressByIdDetailQuery.AddressId
            );
        
        return _mapper.Map<GetAddressDetailDto>(customerFromDatabase);
    }
}