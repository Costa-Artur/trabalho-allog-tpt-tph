using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Univali.Api.Entities;

public class Customer
{
    public int CustomerId {get; set;}

    public string Name {get; set;} = string.Empty;

    public ICollection<Address> Addresses {get; set;} = new List<Address>();

    public string Type { get; set; } = string.Empty;
}