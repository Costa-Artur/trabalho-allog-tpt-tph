using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Features.Customers.Commands.CreateCustomerWithAddress;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerWithAddressCommandHandler : 
    IRequestHandler<CreateCustomerWithAddressCommand, CreateCustomerWithAddressCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;

    private readonly IMapper _mapper;

    private readonly IValidator<CreateCustomerWithAddressCommand> _validator;

    public CreateCustomerWithAddressCommandHandler(
        ICustomerRepository customerRepository, 
        IMapper mapper, 
        IValidator<CreateCustomerWithAddressCommand> validator
    ) {
        _customerRepository = customerRepository;

        _mapper = mapper;

        _validator = validator; 
    }

    public async Task<CreateCustomerWithAddressCommandResponse> Handle(
        CreateCustomerWithAddressCommand request, 
        CancellationToken cancellationToken
    ) {
        CreateCustomerWithAddressCommandResponse createCustomerWithAddressCommandResponse = new();

        var validationResult = _validator.Validate(request);

        if(validationResult.IsValid == false)
        {
            foreach (var error in validationResult.ToDictionary())
            {
                createCustomerWithAddressCommandResponse.Errors.Add(error.Key, error.Value);
            }

            createCustomerWithAddressCommandResponse.IsSuccessful = false;

            return createCustomerWithAddressCommandResponse;
        }

        var customerEntity = _mapper.Map<Customer>(request);

        _customerRepository.AddCustomer(customerEntity);

        await _customerRepository.SaveChangesAsync();

        createCustomerWithAddressCommandResponse.CustomerWithAddress = 
            _mapper.Map<CreateCustomerWithAddressDto>(customerEntity);
        
        return createCustomerWithAddressCommandResponse;
    }
}