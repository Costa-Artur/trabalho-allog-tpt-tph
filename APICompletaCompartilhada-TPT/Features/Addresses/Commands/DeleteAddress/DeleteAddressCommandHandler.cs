using AutoMapper;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Addresses.Commands.DeleteAddress;

public class DeleteAddressCommandHandler : 
    IRequestHandler<DeleteAddressCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteAddressCommandHandler(
        ICustomerRepository customerRepository, 
        IMapper mapper
    ) {
        _customerRepository = customerRepository;
    }

    public async Task Handle(
        DeleteAddressCommand deleteAddressCommand, 
        CancellationToken cancellationToken
    ) {
        var addressEntity = await _customerRepository
            .GetAddressAsync(
                deleteAddressCommand.CustomerId, 
                deleteAddressCommand.AddressId
            );

        if (addressEntity == null) { return; }

        _customerRepository.RemoveAddress(addressEntity);

        await _customerRepository.SaveChangesAsync();
    }
}