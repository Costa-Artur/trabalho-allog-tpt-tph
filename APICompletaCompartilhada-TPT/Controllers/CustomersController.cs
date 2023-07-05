// quando usar operações asyn, geralmente quando tiver operações de I / O
// MULTHREAD É DIFERENTE DE ASSINCRONO
// POIS PODE SER SINCRONO E ASSINCRONO
// Posso aceitar várias requisições sincronas, mas não liberar a thread tem problema de desemepnho

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Univali.Api.DbContexts;
using Univali.Api.Features.Customers.Commands.CreateCustomer;
using Univali.Api.Features.Customers.Commands.CreateCustomerWithAddress;
using Univali.Api.Features.Customers.Commands.DeleteCustomer;
using Univali.Api.Features.Customers.Commands.PartiallyUpdateCustomer;
using Univali.Api.Features.Customers.Commands.UpdateCustomer;
using Univali.Api.Features.Customers.Commands.UpdateCustomerWithAddress;
using Univali.Api.Features.Customers.Queries.GetCustomerDetail;
using Univali.Api.Models;
using Univali.Api.Repositories;

namespace Univali.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/customers")]
public class CustomersController : MainController 
{
    private readonly IMediator _mediator;

    private readonly CustomerContext _customerContext;

    private readonly CustomerRepository _customerRepository;

    public CustomersController(
        IMediator mediator,
        CustomerContext customerContext,
        CustomerRepository customerRepository
    ) {
        _mediator = mediator;

        _customerRepository = customerRepository;

        _customerContext = customerContext;
    }

// TPT
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<object>>> GetCustomersAsync() 
    {
        var getCustomerQuery = new GetCustomersQuery();

        var customerResponse = await _mediator.Send(getCustomerQuery);

        if (customerResponse.IsSuccessful == false) { return NoContent(); }
            
        return Ok(customerResponse.Customers);
    }

    [HttpGet("{customerId}", Name = "GetCustomerById")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<object>> GetCustomerById(int customerId) 
    {
        var getCustomerQuery = new GetCustomerByIdQuery { CustomerId = customerId };

        var customerResponse = await _mediator.Send(getCustomerQuery);

        if (customerResponse.IsSuccessful == false) return NotFound();

        return Ok(customerResponse.Customer);
    }

/*
    [HttpGet("cpf/{cpf}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CustomerDto>> GetCustomerByCpfAsync(string cpf) 
    {
        var getCustomerQuery = new GetCustomerByCpfQuery { Cpf = cpf };

        var customerToReturn = await _mediator.Send(getCustomerQuery);

        if (customerToReturn == null) return NotFound();

        return Ok(customerToReturn);
    }
*/

// TPT
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Object>> CreateCustomerAsync(
        CreateCustomerCommand createCustomerCommand) 
    {
        var createCustomerCommandResponse = await _mediator.Send(createCustomerCommand);

        if (createCustomerCommandResponse.IsSuccessful == false) 
        {
            if (createCustomerCommandResponse.Errors.Count() == 0) 
            {
                return NotFound();
            }

            return Validate(createCustomerCommandResponse.Errors);
        }

        return Ok(createCustomerCommandResponse.Customer);

/*
        return CreatedAtRoute(
            "GetCustomerById",
            new { customerId = createCustomerCommandResponse.Customer.CustomerId },
            createCustomerCommandResponse.Customer);
*/
    }

    [HttpPut("{customerId}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> UpdateCustomerAsync(
        int customerId, 
        UpdateCustomerCommand updateCustomerCommand
    ) {
        if(customerId != updateCustomerCommand.CustomerId) { return BadRequest(); }

        var UpdateCustomerCommandResponse = await _mediator.Send(updateCustomerCommand);

        if (UpdateCustomerCommandResponse.IsSuccessful == false) 
        {
            return Validate(UpdateCustomerCommandResponse.Errors);
        }

        return NoContent();
    }

    [HttpDelete("{customerId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteCustomerAsync(int customerId)
    {
        await _mediator.Send(
            new DeleteCustomerCommand { CustomerId = customerId }
        );

        return NoContent();
    }

    [HttpPatch("{customerId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<PartiallyUpdateCustomerCommandResponse>> 
        PartiallyUpdateCustomer(
            [FromBody] JsonPatchDocument<PartiallyUpdateCustomerDto> patchDocument, 
            [FromRoute] int customerId
    ) {
        var customerExists = await _customerRepository.CustomerExistsAsync(customerId);

        if(!customerExists) return NotFound();

        var partiallyUpdateCustomerCommand = 
            new PartiallyUpdateCustomerCommand { 
                CustomerId = customerId, PatchDocument = patchDocument 
            };

        var partiallyUpdateCustomerCommandResponse = 
            await _mediator.Send(partiallyUpdateCustomerCommand);

        if (partiallyUpdateCustomerCommandResponse.IsSuccessful == false) 
        {
            if (partiallyUpdateCustomerCommandResponse.Errors.Count() == 0) 
            {
                return NotFound();
            }

            return Validate(partiallyUpdateCustomerCommandResponse.Errors);
        }

        return NoContent();
    }


// WITH ADDRESSES

    [HttpGet("with_addresses")]
    public async Task<ActionResult<IEnumerable<GetCustomerWithAddressesDto>>> 
        GetCustomersWithAddressesAsync()
    {
        var customersToReturn = await _mediator.Send(
            new GetCustomersWithAddressesQuery()
        );

        return Ok(customersToReturn);
    }

    [HttpGet("{customerId}/with_addresses", Name = "getByIdWithAddresses")]
    public async Task<ActionResult<GetCustomerWithAddressesDto>> 
        GetCustomerByIdWithAddresses(int customerId)
    {
        var getCustomerWithAddressByIdQuery = await _mediator.Send(
            new GetCustomerWithAddressByIdQuery { CustomerId = customerId }
        );

        if (getCustomerWithAddressByIdQuery.IsSuccessful == false) 
        {
            if (getCustomerWithAddressByIdQuery.Errors.Count() == 0)
            {
                return NotFound();
            }
            return Validate(getCustomerWithAddressByIdQuery.Errors);
        }
        
        return Ok(getCustomerWithAddressByIdQuery.Customer);
    }

/*
    [HttpGet("cpf/{cpf}/with_addresses")]
    public async Task<ActionResult<GetCustomerWithAddressesDto>> 
        GetCustomerByCpfWithAddresses(string cpf)
    {
        var customerToReturn = await _mediator.Send(
            new GetCustomerWithAddressByCpfQuery { Cpf = cpf }
        );

        return Ok(customerToReturn);
    }
    */

    [HttpPost("with_addresses")] 
    public async Task<ActionResult<CreateCustomerWithAddressDto>> CreateCustomerWithAddresses(
        CreateCustomerWithAddressCommand createCustomerWithAddressCommand
    ) {
        var createCustomerWithAddressCommandResponse = 
            await _mediator.Send(createCustomerWithAddressCommand);

        if (createCustomerWithAddressCommandResponse.IsSuccessful == false) 
        {
            if (createCustomerWithAddressCommandResponse.Errors.Count() == 0) 
            {
                return NotFound(); 
            }

            return Validate(createCustomerWithAddressCommandResponse.Errors);
        }

        return CreatedAtRoute(
            "getByIdWithAddresses",
            new { 
                customerId = createCustomerWithAddressCommandResponse.CustomerWithAddress.CustomerId 
            },
            createCustomerWithAddressCommandResponse.CustomerWithAddress
        );
    }

    [HttpPut("{customerId}/with_addresses")]
    public async Task<ActionResult> UpdateCustomerWithAddress(
        int customerId, 
        UpdateCustomerWithAddressCommand updateCustomerWithAddressCommand
    ) {


        //checa se address existe
        foreach(var address in updateCustomerWithAddressCommand.Addresses)
        {
            var addressExists = await _customerRepository.GetAddressAsync(updateCustomerWithAddressCommand.CustomerId,address.AddressId);
            if (addressExists == null){
                return NotFound();
            }
            
        }

        var updateCustomerWithAddressCommandResponse =  
            await _mediator.Send(updateCustomerWithAddressCommand);

        if (updateCustomerWithAddressCommandResponse.IsSuccessful == false) 
        {
            if (updateCustomerWithAddressCommandResponse.Errors.Count() == 0) 
            {
                return NotFound(); 
            }

            return Validate(updateCustomerWithAddressCommandResponse.Errors);
        }

        return NoContent();
    }
}
