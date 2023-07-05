namespace Univali.Api.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerDto
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string Cpf { get; set; } = string.Empty;
}
