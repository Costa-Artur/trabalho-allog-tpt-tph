/*
using AutoMapper;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;

public class GetCustomerByCpfQueryHandler
    : IRequestHandler<GetCustomerByCpfQuery, GetCustomerDto>
{
    private readonly ICustomerRepository _customerRepository;

    private readonly IMapper _mapper;

    public GetCustomerByCpfQueryHandler(
        ICustomerRepository customerRepository, 
        IMapper mapper
    ) {
        _customerRepository = customerRepository;
        
        _mapper = mapper;
    }

    public async Task<GetCustomerDto> Handle(
        GetCustomerByCpfQuery getCustomerByCpfQuery, 
        CancellationToken cancellationToken
    ) {
        var customerFromDatabase = await _customerRepository
            .GetCustomerByCpfAsync(getCustomerByCpfQuery.Cpf);

        return _mapper.Map<GetCustomerDto>(customerFromDatabase);
    }
}
*/