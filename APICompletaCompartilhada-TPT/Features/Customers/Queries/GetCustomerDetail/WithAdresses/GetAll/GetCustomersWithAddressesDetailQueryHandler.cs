using AutoMapper;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;

public class GetCustomerWithAddressQueryHandler : 
    IRequestHandler<GetCustomersWithAddressesQuery, List<GetCustomerWithAddressesDto>>
{
    private readonly ICustomerRepository _customerRepository;

    private readonly IMapper _mapper;

    public GetCustomerWithAddressQueryHandler(
        ICustomerRepository customerRepository, 
        IMapper mapper
    ) {
        _customerRepository = customerRepository;

        _mapper = mapper;
    }


    public async Task<List<GetCustomerWithAddressesDto>> Handle(
        GetCustomersWithAddressesQuery getCustomersWithAddressesDetailQuery, 
        CancellationToken cancellationToken
    ) {
        var customersFromDatabase = await _customerRepository
            .GetCustomersWithAddressesAsync();

        return _mapper
            .Map<List<GetCustomerWithAddressesDto>>(customersFromDatabase);
    }
}