/*
using AutoMapper;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;

public class GetCustomerWithAddressByCpfQueryHandler
    : IRequestHandler<GetCustomerWithAddressByCpfQuery, GetCustomerWithAddressesDto>
{
    private readonly ICustomerRepository _customerRepository;

    private readonly IMapper _mapper;

    public GetCustomerWithAddressByCpfQueryHandler(
        ICustomerRepository customerRepository, 
        IMapper mapper
    ) {
        _customerRepository = customerRepository;

        _mapper = mapper;
    }

    public async Task<GetCustomerWithAddressesDto> Handle(
        GetCustomerWithAddressByCpfQuery getCustomerWithAddressByCpfDetailQuery, 
        CancellationToken cancellationToken
    ) {
        var customerFromDatabase = await _customerRepository
            .GetCustomerByCpfWithAddressesAsync(
                getCustomerWithAddressByCpfDetailQuery.Cpf
            );

        return _mapper.Map<GetCustomerWithAddressesDto>(customerFromDatabase);
    }
}
*/