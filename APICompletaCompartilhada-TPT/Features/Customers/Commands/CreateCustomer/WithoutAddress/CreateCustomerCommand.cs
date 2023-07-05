// TPT
using MediatR;

namespace Univali.Api.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest<CreateCustomerCommandResponse>
{
    public string Name {get; set;} = string.Empty;

    public string? CPF {get; set;}

    public string? CNPJ {get; set;}


}

