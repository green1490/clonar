using System.ComponentModel.DataAnnotations;

namespace Entity;

public class Login
{
    [Required]
    [EmailAddress]
    public string Email {get;set;} = string.Empty;

    [Required]
    [MaxLength(12)]
    public string Password {get;set;} = string.Empty;
}