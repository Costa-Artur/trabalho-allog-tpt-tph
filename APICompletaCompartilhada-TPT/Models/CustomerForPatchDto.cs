using System.ComponentModel.DataAnnotations;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Models;

public class CustomerForPatchDto
{
    [Required(ErrorMessage = "You should fill out a Name")]
    [MaxLength(100, ErrorMessage = "The name shouldn't have more than 100 characters")]
    public string Name { get; set; } = string.Empty;


    [Required(ErrorMessage = "You should fill out a Cpf")]
    [CPFValidationAttribute(ErrorMessage = "CPF must be valid")]
    public string Cpf { get; set; } = string.Empty;
}