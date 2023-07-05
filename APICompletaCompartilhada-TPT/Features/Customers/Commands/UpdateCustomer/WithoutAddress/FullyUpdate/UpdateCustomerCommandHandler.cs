using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler : 
    IRequestHandler<UpdateCustomerCommand, UpdateCustomerCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;

    private readonly IMapper _mapper;

    public readonly IValidator<UpdateCustomerCommand> _validator;

    public UpdateCustomerCommandHandler(
        ICustomerRepository customerRepository, 
        IMapper mapper,
        IValidator<UpdateCustomerCommand> validator
    ) {
        _customerRepository = customerRepository;

        _mapper = mapper;

        _validator = validator;
    }

    public async Task<UpdateCustomerCommandResponse> Handle(
        UpdateCustomerCommand updateCustomerCommand, 
        CancellationToken cancellationToken
    ) {
        UpdateCustomerCommandResponse updateCustomerCommandResponse = new();

        var validationResult = _validator.Validate(updateCustomerCommand);

        if (!validationResult.IsValid) 
        {
            foreach (var error in validationResult.ToDictionary()) 
            {
                updateCustomerCommandResponse.Errors
                    .Add(error.Key, error.Value);
            }

            updateCustomerCommandResponse.IsSuccessful = false;

            return updateCustomerCommandResponse;
        }

        var customerEntity = await _customerRepository
            .GetCustomerByIdAsync(updateCustomerCommand.CustomerId);

        _mapper.Map(updateCustomerCommand, customerEntity);

        await _customerRepository.SaveChangesAsync();

        return new UpdateCustomerCommandResponse();
    }
}