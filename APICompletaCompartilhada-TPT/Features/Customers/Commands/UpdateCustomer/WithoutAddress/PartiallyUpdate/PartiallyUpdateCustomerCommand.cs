using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Univali.Api.Models;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.Customers.Commands.PartiallyUpdateCustomer;

public class PartiallyUpdateCustomerCommand : 
  IRequest<PartiallyUpdateCustomerCommandResponse>
{
  public int CustomerId { get; set; }

  public JsonPatchDocument<PartiallyUpdateCustomerDto> PatchDocument { get; set; } = null!;
}

