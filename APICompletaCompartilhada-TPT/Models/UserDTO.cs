using System.ComponentModel.DataAnnotations;

namespace Univali.Api.Models;

public class UserDTO
{
    [Required(ErrorMessage = "Error: Username cannot be empty")]
    [MaxLength(50, ErrorMessage = "Error: Username must be 50 characters or less")]

    public string Username { get; set; } = string.Empty;
    

    [Required(ErrorMessage = "Error: Password cannot be empty")]
    public string Password { get; set; } = string.Empty;
}