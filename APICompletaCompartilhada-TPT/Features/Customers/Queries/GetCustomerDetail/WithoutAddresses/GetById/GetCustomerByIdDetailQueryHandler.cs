// TPT
using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;

public class GetCustomerByIdQueryHandler : 
    IRequestHandler<GetCustomerByIdQuery, GetCustomerDetailQueryResponse>
{
    private readonly ICustomerRepository _customerRepository;

    private readonly IMapper _mapper;

    public GetCustomerByIdQueryHandler(
        ICustomerRepository customerRepository, 
        IMapper mapper
    ) {
        _customerRepository = customerRepository;

        _mapper = mapper;
    }

// APENAS PARA TESTES
    public async Task<GetCustomerDetailQueryResponse> Handle(
        GetCustomerByIdQuery getCustomerByIdQuery, 
        CancellationToken cancellationToken
    ) {
        GetCustomerDetailQueryResponse getCustomerDetailQueryResponse = new ();

        var customerFromDatabase = await _customerRepository
            .GetCustomerByIdAsync(getCustomerByIdQuery.CustomerId);

        if (customerFromDatabase == null) {
            getCustomerDetailQueryResponse.IsSuccessful = false;

            return getCustomerDetailQueryResponse;
        }

        if (customerFromDatabase is NaturalCustomer naturalCustomer)
        {
            getCustomerDetailQueryResponse.Customer =  _mapper.Map<GetNaturalCustomersDetailDto>(naturalCustomer);
            return getCustomerDetailQueryResponse;
        }
        else if (customerFromDatabase is LegalCustomer legalCustomer)
        {
            getCustomerDetailQueryResponse.Customer = _mapper.Map<GetLegalCustomersDetailDto>(legalCustomer);
            return getCustomerDetailQueryResponse;
        }
       
       return getCustomerDetailQueryResponse;
    }
}