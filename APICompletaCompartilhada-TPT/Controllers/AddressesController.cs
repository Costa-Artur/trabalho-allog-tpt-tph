
using Microsoft.AspNetCore.Mvc;
using Univali.Api.Models;
using Univali.Api.Repositories;
using MediatR;
using Univali.Api.Features.Addresses.Queries.GetCustomerDetail;
using Univali.Api.Features.Addresses.Commands.CreateAddress;
using Univali.Api.Features.Addresses.Commands.DeleteAddress;
using Univali.Api.Features.Addresses.Commands.UpdateAddresses;
using Microsoft.AspNetCore.Authorization;

namespace Univali.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/customers/{customerId}/addresses")]
public class AddressController : MainController
{
    private readonly ICustomerRepository _customerRepository;

    private readonly IMediator _mediator;

    public AddressController(
        ICustomerRepository customerRepository, 
        IMediator mediator
    ) {
        _customerRepository = customerRepository;

        _mediator = mediator;
    }

    // <summary>
    // Retorna todos os endereços do consumidor informado na rota
    // </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<IEnumerable<GetAddressDetailDto>>> GetAddresses(
        int customerId
    ) {
        var addressesToReturn = await _mediator.Send(
            new GetAddressDetailQuery { CustomerId = customerId }
        );

        if (addressesToReturn.Count() == 0) { return NoContent(); } 
        
        return Ok(addressesToReturn);
    }

    [HttpGet("{addressId}", Name = "GetAddress")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetAddressDetailDto>> GetAddress(
        int customerId, 
        int addressId
    ) {
        var addressToReturn = await _mediator.Send(
            new GetAddressByIdDetailQuery { CustomerId = customerId, AddressId = addressId } 
        );

        if (addressToReturn == null) { return NotFound(); } 

        return Ok(addressToReturn);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<List<AddressDto>>> CreateAddress(
        int customerId,
        CreateAddressCommand createAddressCommand
    ) {
        if (customerId != createAddressCommand.CustomerId) {
            return BadRequest();
        }

       var createAddressCommandResponse = await _mediator.Send(createAddressCommand);


        if (createAddressCommandResponse.IsSuccessful == false) 
        {
            if (createAddressCommandResponse.Errors.Count() == 0) 
            {
                return NotFound(); 
            }

            return Validate(createAddressCommandResponse.Errors);
        }
        
        return CreatedAtRoute(
            "GetAddress",
            new { 
                addressId = createAddressCommandResponse.Address.AddressId, 
                customerId = createAddressCommandResponse.Address.CustomerId 
            },
            createAddressCommand
        );

    }

    [HttpPut("{addressId}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> UpdateAddress(
        int customerId,
        int addressId,
        UpdateAddressCommand updateAddressCommand
    ) {    
        if (customerId != updateAddressCommand.CustomerId
            || addressId != updateAddressCommand.AddressId) {
            return BadRequest();
        }

        var updateAddressCommandResponse = await _mediator.Send(updateAddressCommand);


        if (updateAddressCommandResponse.IsSuccessful == false) 
        {
            if (updateAddressCommandResponse.Errors.Count() == 0) 
            {
                return NotFound(); 
            }

            return Validate(updateAddressCommandResponse.Errors);
        }
        
        return Ok();
    }

    [HttpDelete("{addressId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteAddress (int customerId, int addressId)
    {
        await _mediator.Send(
            new DeleteAddressCommand {CustomerId = customerId, AddressId = addressId}
        );
        
        return NoContent();
    }
}
