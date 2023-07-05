namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;

public class GetNaturalCustomersDetailDto : GetCustomerDto
{
    public string CPF { get; set; } = string.Empty;
    public string CustomerType {get;set;} = "NaturalCustomer";
}