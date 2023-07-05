using AutoMapper;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Commands.DeleteCustomer;

// O primeiro parâmetro é o tipo da mensagem
// O segundo parâmetro é o tipo que se espera receber.
public class DeleteCustomerCommandHandler : 
    IRequestHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteCustomerCommandHandler(
        ICustomerRepository customerRepository, 
        IMapper mapper
    ) {
        _customerRepository = customerRepository;
    }

    // Preciso retornar um boleano
    public async Task Handle(
        DeleteCustomerCommand deleteCustomerCommand, 
        CancellationToken cancellationToken
    ) {
        var customerEntity = await _customerRepository
            .GetCustomerByIdAsync(deleteCustomerCommand.CustomerId);

        if (customerEntity == null) { return; }

        _customerRepository.RemoveCustomer(customerEntity);

        await _customerRepository.SaveChangesAsync();
    }
}