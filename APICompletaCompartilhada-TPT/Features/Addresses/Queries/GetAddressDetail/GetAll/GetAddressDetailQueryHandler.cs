using AutoMapper;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Addresses.Queries.GetCustomerDetail;

public class GetAddressDetailQueryHandler : 
    IRequestHandler<GetAddressDetailQuery, List<GetAddressDetailDto>>
{
    private readonly ICustomerRepository _customerRepository;
    
    private readonly IMapper _mapper;

    public GetAddressDetailQueryHandler(
        ICustomerRepository customerRepository, 
        IMapper mapper
    ) {
        _customerRepository = customerRepository;

        _mapper = mapper;
    }


    public async Task<List<GetAddressDetailDto>> Handle(
        GetAddressDetailQuery getAddressDetailQuery, 
        CancellationToken cancellationToken
    ) {
        var addressesFromDatabase = await _customerRepository
            .GetAddressesAsync(getAddressDetailQuery.CustomerId);

        return _mapper.Map<List<GetAddressDetailDto>>(addressesFromDatabase);
    }
}