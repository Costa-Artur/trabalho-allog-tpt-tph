using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Models;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Commands.PartiallyUpdateCustomer;

public class PartiallyUpdateCustomerCommandHandler : 
  IRequestHandler<PartiallyUpdateCustomerCommand, PartiallyUpdateCustomerCommandResponse>
{
  private readonly ICustomerRepository _customerRepository;

  private readonly IMapper _mapper;

  private readonly IValidator<PartiallyUpdateCustomerDto> _validator;

  public PartiallyUpdateCustomerCommandHandler(
    ICustomerRepository customerRepository, 
    IMapper mapper, 
    IValidator<PartiallyUpdateCustomerDto> validator
  ) {
      _customerRepository = customerRepository;

      _mapper = mapper;

      _validator = validator;
  }

  public async Task<PartiallyUpdateCustomerCommandResponse> Handle(
    PartiallyUpdateCustomerCommand request, 
    CancellationToken cancellationToken
  ) {
    var customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(request.CustomerId);

    var customerToPatch = _mapper.Map<PartiallyUpdateCustomerDto>(customerFromDatabase); //Tentar tirar do model.

    request.PatchDocument.ApplyTo(customerToPatch);

    PartiallyUpdateCustomerCommandResponse updateCustomerCommandResponse = new();

        var validationResult = _validator.Validate(customerToPatch);

        if(!validationResult.IsValid)
        {
            foreach(var error in validationResult.ToDictionary())
            {
                updateCustomerCommandResponse.Errors.Add(error.Key, error.Value);
            }

            updateCustomerCommandResponse.IsSuccessful = false;

            return updateCustomerCommandResponse;
        }

    _mapper.Map(customerToPatch, customerFromDatabase);

    await _customerRepository.SaveChangesAsync();

    return updateCustomerCommandResponse;
  }
}