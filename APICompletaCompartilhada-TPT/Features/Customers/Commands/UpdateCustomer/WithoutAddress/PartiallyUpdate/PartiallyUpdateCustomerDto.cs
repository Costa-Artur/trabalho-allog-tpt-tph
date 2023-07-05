using Univali.Api.Models;

namespace Univali.Api.Features.Customers.Commands.PartiallyUpdateCustomer;

public class PartiallyUpdateCustomerDto
{
    public string Name { get; set; } = string.Empty;
    
    public string Cpf { get; set; } = string.Empty;
}

