// TPT
namespace Univali.Api.Entities;

public class LegalCustomer : Customer 
{
    public string CNPJ { get; set; } = string.Empty;
}