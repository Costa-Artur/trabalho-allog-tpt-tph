using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Features.Addresses.Commands.UpdateCustomerWithAddress;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Commands.UpdateCustomerWithAddress;

public class UpdateCustomerWithAddressCommandHandler : 
    IRequestHandler<UpdateCustomerWithAddressCommand, UpdateCustomerWithAddressCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;

    private readonly IMapper _mapper;

    private readonly IValidator<UpdateCustomerWithAddressCommand> _validator;


    public UpdateCustomerWithAddressCommandHandler(
        ICustomerRepository customerRepository, 
        IMapper mapper, 
        IValidator<UpdateCustomerWithAddressCommand> validator
    ) {
        _customerRepository = customerRepository;

        _mapper = mapper;

        _validator = validator;
    }

    public async Task<UpdateCustomerWithAddressCommandResponse> Handle(
        UpdateCustomerWithAddressCommand updateCustomerWithAddressCommand,
        CancellationToken cancellationToken
    ) {
        UpdateCustomerWithAddressCommandResponse updateCustomerWithAddressCommandResponse = new();

        var validationResult = _validator.Validate(updateCustomerWithAddressCommand);

        if (validationResult.IsValid == false)
        {
            foreach (var error in validationResult.ToDictionary())
            {
                updateCustomerWithAddressCommandResponse.Errors.Add(error.Key, error.Value);
            }

            updateCustomerWithAddressCommandResponse.IsSuccessful = false;

            return updateCustomerWithAddressCommandResponse;
        }


        var customerEntity = await _customerRepository
            .GetCustomerByIdAsync(updateCustomerWithAddressCommand.CustomerId);

        _mapper.Map(updateCustomerWithAddressCommand, customerEntity);

        await _customerRepository.SaveChangesAsync();

        return updateCustomerWithAddressCommandResponse;
    }
}