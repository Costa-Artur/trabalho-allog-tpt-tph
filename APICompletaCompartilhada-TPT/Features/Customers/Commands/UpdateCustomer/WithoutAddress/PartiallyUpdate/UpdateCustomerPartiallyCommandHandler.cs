using AutoMapper;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerPartiallyCommandHandler : 
    IRequestHandler<UpdateCustomerPartiallyCommand>
{
    private readonly ICustomerRepository _customerRepository;
    
    private readonly IMapper _mapper;

    public UpdateCustomerPartiallyCommandHandler(
        ICustomerRepository customerRepository, 
        IMapper mapper
    ) {
        _customerRepository = customerRepository;

        _mapper = mapper;
    }

    public async Task Handle(
        UpdateCustomerPartiallyCommand updateCustomerPartiallyCommand, 
        CancellationToken cancellationToken
    ) {
        var customerFromDatabase = await _customerRepository
            .GetCustomerByIdAsync(updateCustomerPartiallyCommand.CustomerId);

        if(customerFromDatabase == null) { return; }

        //request.ApplyTo(customerFromDatabase);

        await _customerRepository.SaveChangesAsync();
    }
}