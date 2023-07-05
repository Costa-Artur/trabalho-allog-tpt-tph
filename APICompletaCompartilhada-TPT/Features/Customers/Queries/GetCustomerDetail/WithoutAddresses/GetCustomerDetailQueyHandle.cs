using AutoMapper;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;

public class GetCustomerQueryHandle : IGetCustomerQueryHandle 
{
    private readonly ICustomerRepository _customerRepository;
    
    private readonly IMapper _mapper;

    public GetCustomerQueryHandle(
        ICustomerRepository customerRepository,
        IMapper mapper
    ) {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<GetCustomerDto?> Handle(
        GetCustomerQuery getCustomerQuery
    ) {
        var customerFromDatabase = await _customerRepository
            .GetCustomerByIdAsync(getCustomerQuery.CustomerId);
            
        return _mapper.Map<GetCustomerDto>(customerFromDatabase);
    }
}