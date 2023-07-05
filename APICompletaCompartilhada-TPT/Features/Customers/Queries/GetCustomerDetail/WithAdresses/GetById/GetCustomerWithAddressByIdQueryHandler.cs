using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;

public class GetCustomerWithAddressByIdDetailQueryHandler : 
    IRequestHandler<GetCustomerWithAddressByIdQuery, GetCustomerWithAddressByIdQueryResponse?>
{
    private readonly ICustomerRepository _customerRepository;

    private readonly IMapper _mapper;

    public readonly IValidator<GetCustomerWithAddressByIdQuery> _validator;

    public GetCustomerWithAddressByIdDetailQueryHandler(
        ICustomerRepository customerRepository, 
        IMapper mapper,
        IValidator<GetCustomerWithAddressByIdQuery> validator
    ) {
        _customerRepository = customerRepository;

        _mapper = mapper;

        _validator = validator;
    }

    public async Task<GetCustomerWithAddressByIdQueryResponse?> Handle(
        GetCustomerWithAddressByIdQuery getCustomerWithAddressByIdDetailQuery, 
        CancellationToken cancellationToken
    ) {
        GetCustomerWithAddressByIdQueryResponse getCustomerWithAddressByIdQueryResponse = new();

        var validationResult = _validator.Validate(getCustomerWithAddressByIdDetailQuery);

        if (validationResult.IsValid == false) 
        {
            foreach (var error in validationResult.ToDictionary()) 
            {
                getCustomerWithAddressByIdQueryResponse.Errors
                    .Add(error.Key, error.Value);
            }

            getCustomerWithAddressByIdQueryResponse.IsSuccessful = false;

            return getCustomerWithAddressByIdQueryResponse;
        }

        var customerFromDatabase = await _customerRepository
            .GetCustomerByIdWithAddressesAsync(
                getCustomerWithAddressByIdDetailQuery.CustomerId
            );
        
        if (customerFromDatabase == null) { 
            getCustomerWithAddressByIdQueryResponse.IsSuccessful = false;

            return getCustomerWithAddressByIdQueryResponse;
        }

        getCustomerWithAddressByIdQueryResponse.Customer = _mapper
            .Map<GetCustomerWithAddressesDto>(customerFromDatabase);

        return getCustomerWithAddressByIdQueryResponse;
    }
}