namespace Univali.Api.Models;

public class AddressDto
{
  public int AddressId {get; set;}

  public string Street {get; set;} = string.Empty;

  public string City {get; set;} = string.Empty;
  
  public int CustomerId {get; set;}
}
