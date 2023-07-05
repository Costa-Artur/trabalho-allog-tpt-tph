// FILTRO
using Microsoft.AspNetCore.Mvc;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Controllers;

[ApiController]
[Route("api/customers-collection")]
public class CustomersCollectionController : MainController
{
    private readonly ICustomerRepository _customerRepository;

    public CustomersCollectionController(
        ICustomerRepository customerRepository
    ) {
        _customerRepository = customerRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers(
        string? category,
        string searchQuery = ""
    ) {
        return Ok(await _customerRepository.GetCustomersCollection(category, searchQuery));
    }
}