// TPT
using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : 
    IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;
    
    private readonly IMapper _mapper;
    
    public readonly IValidator<CreateCustomerCommand> _validator;

    public CreateCustomerCommandHandler(
        ICustomerRepository customerRepository, 
        IMapper mapper,
        IValidator<CreateCustomerCommand> validator
    ) {
        _customerRepository = customerRepository;

        _mapper = mapper;
        
        _validator = validator;
    }

    public async Task<CreateCustomerCommandResponse> Handle(
        CreateCustomerCommand createCustomerCommand, 
        CancellationToken cancellationToken
    ) {
        CreateCustomerCommandResponse createCustomerCommandResponse = new();

        var validationResult = _validator.Validate(createCustomerCommand);

        if (validationResult.IsValid == false) 
        {
            foreach (var error in validationResult.ToDictionary()) 
            {
                createCustomerCommandResponse.Errors
                    .Add(error.Key, error.Value);
            }

            createCustomerCommandResponse.IsSuccessful = false;

            return createCustomerCommandResponse;
        }

        if (string.IsNullOrWhiteSpace(createCustomerCommand.CPF)) {
            if (string.IsNullOrWhiteSpace(createCustomerCommand.CNPJ)) {
                // CPF E CNPJ SÃO NULOS
                createCustomerCommandResponse.IsSuccessful = false;

                return createCustomerCommandResponse;
            }
            var legalCustomer = _mapper.Map<LegalCustomer>(createCustomerCommand);

            _customerRepository.AddCustomer(legalCustomer);
            await _customerRepository.SaveChangesAsync();
            createCustomerCommandResponse.Customer = legalCustomer;
            return createCustomerCommandResponse;
        }
        else if (string.IsNullOrWhiteSpace(createCustomerCommand.CNPJ)) {
            var naturalCustomer = _mapper.Map<NaturalCustomer>(createCustomerCommand);

            _customerRepository.AddCustomer(naturalCustomer);
            await _customerRepository.SaveChangesAsync();
            createCustomerCommandResponse.Customer = naturalCustomer;
            return createCustomerCommandResponse;
        }
        else {
            // CPF E CNPJ INFORMADOS / ambiguidade
            createCustomerCommandResponse.IsSuccessful = false;

            return createCustomerCommandResponse;
        }
    }
}