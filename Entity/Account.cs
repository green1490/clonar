using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity;

[Table("account")]
public class Account
{
    [Key]
    public int id {get; set;}

    [Required]
    [EmailAddress]
    public String email {get;set;} = String.Empty;

    [Required]
    [MaxLength(10)]
    public String username {get;set;} = String.Empty;

    [Required]
    [MaxLength(12)]
    public String password {get;set;} = String.Empty;

    public int karma {get;set;}
}