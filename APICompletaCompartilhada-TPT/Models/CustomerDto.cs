namespace Univali.Api.Models;

public class CustomerDto
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string Cpf { get; set; } = string.Empty;
}
