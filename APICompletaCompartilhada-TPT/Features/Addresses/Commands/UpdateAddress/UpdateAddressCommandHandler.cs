using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Addresses.Commands.UpdateAddresses;

public class UpdateAddressCommandHandler : 
    IRequestHandler<UpdateAddressCommand, 
    UpdateAddressCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;

    private readonly IMapper _mapper;

    private readonly IValidator<UpdateAddressCommand> _validator;

    public UpdateAddressCommandHandler(
        ICustomerRepository customerRepository, 
        IMapper mapper, 
        IValidator<UpdateAddressCommand> validator
    ) {
        _validator = validator;

        _customerRepository = customerRepository;

        _mapper = mapper;
    }

    public async Task<UpdateAddressCommandResponse> Handle(
        UpdateAddressCommand request, 
        CancellationToken cancellationToken
    ) {
        UpdateAddressCommandResponse updateAddressCommandResponse = new();

        var validationResult = _validator.Validate(request);

        if(validationResult.IsValid == false)
        {
            foreach (var error in validationResult.ToDictionary())
            {
                updateAddressCommandResponse.Errors.Add(error.Key, error.Value);
            }

            updateAddressCommandResponse.IsSuccessful = false;

            return updateAddressCommandResponse;
        }

        var addressEntity = await _customerRepository.GetAddressAsync(request.CustomerId, request.AddressId);

        _mapper.Map(request, addressEntity);

        await _customerRepository.SaveChangesAsync();

        return updateAddressCommandResponse;
    }

    

}