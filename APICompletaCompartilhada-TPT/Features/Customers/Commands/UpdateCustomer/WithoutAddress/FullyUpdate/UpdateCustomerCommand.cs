using MediatR;

namespace Univali.Api.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommand : IRequest<UpdateCustomerCommandResponse>
{
    public int CustomerId {get; set;} // validação apenas

    public string Name {get; set;} = string.Empty;

    public string Cpf {get; set;} = string.Empty;
}

