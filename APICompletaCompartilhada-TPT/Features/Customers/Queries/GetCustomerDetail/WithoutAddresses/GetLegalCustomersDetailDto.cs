namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;

public class GetLegalCustomersDetailDto : GetCustomerDto
{
    public string CNPJ { get; set; } = string.Empty;
    public string CustomerType {get;set;} = "LegalCustomer";
}