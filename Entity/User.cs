using System.ComponentModel.DataAnnotations;

namespace Entity;

public class User
{
    public int ID {get; set;}

    [Required]
    [EmailAddress]
    public String Email {get;set;} = String.Empty;

    [Required]
    [MaxLength(10)]
    public String UserName {get;set;} = String.Empty;

    [Required]
    [MaxLength(12)]
    public String Password {get;set;} = String.Empty;

    [Required]
    public int Karma {get;set;}
}