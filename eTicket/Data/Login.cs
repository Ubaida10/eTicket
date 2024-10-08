using System.ComponentModel.DataAnnotations;

namespace eTicket.Data;

public class Login
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    public bool RememberMe { get; set; }
    public string Role { get; set; }
}