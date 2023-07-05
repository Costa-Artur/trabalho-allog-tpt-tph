using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Univali.Api.Entities;

public class Address
{
   //[Key]
   //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
   public int AddressId { get; set;}

   //[Required]
   //[MaxLength(50)]
   public string Street {get; set;} = string.Empty;

   //[Required]
   //[MaxLength(50)]
   public string City {get; set;} = string.Empty;

   //[ForeignKey("CustomerId")]
   public int CustomerId {get; set;}

   public Customer? customer {get; set;}
}
