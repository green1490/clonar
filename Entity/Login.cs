using System.ComponentModel.DataAnnotations;

namespace Entity;

public class Login
{
    [EmailAddress]
    public string Email {get;set;} = string.Empty;

    [MaxLength(12)]
    public string Password {get;set;} = string.Empty;
}