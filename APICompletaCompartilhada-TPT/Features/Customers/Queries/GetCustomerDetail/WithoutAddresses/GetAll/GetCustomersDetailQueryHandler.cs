// TPT
using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;

public class GetCustomerQueryHandler : 
    IRequestHandler<GetCustomersQuery, GetCustomersDetailQueryResponse>
{
    private readonly ICustomerRepository _customerRepository;
    
    private readonly IMapper _mapper;

    public GetCustomerQueryHandler(
        ICustomerRepository customerRepository, 
        IMapper mapper
    ) {
        _customerRepository = customerRepository;

        _mapper = mapper;
    }

// APENAS PARA TESTES

    public async Task<GetCustomersDetailQueryResponse> Handle(
        GetCustomersQuery getCustomersQuery, 
        CancellationToken cancellationToken
    ) {
        GetCustomersDetailQueryResponse getCustomersDetailQueryResponse = new ();

        var customersFromDatabase = await _customerRepository.GetCustomersAsync();

        if (customersFromDatabase == null)
        {
            getCustomersDetailQueryResponse.IsSuccessful = false;

            return getCustomersDetailQueryResponse;
        }

        List<Object> customersToReturn = new();

        foreach (Customer customer in customersFromDatabase) 
        {
            if (customer is LegalCustomer legalCustomer) 
            {
                var legalCustomerToReturn = _mapper.Map<GetLegalCustomersDetailDto>(legalCustomer);
                customersToReturn.Add(legalCustomerToReturn);
            } 
            else if (customer is NaturalCustomer naturalCustomer) 
            {
                var naturalCustomerToReturn =  _mapper.Map<GetNaturalCustomersDetailDto>(naturalCustomer);
                customersToReturn.Add(naturalCustomerToReturn);
            } 
        }

        getCustomersDetailQueryResponse.Customers = customersToReturn;

        return getCustomersDetailQueryResponse;
    }
}