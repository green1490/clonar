using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity;

[Table("account")]
public class Account
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Column("id")]
    public int ID {get; set;}

    [Required]
    [EmailAddress]
    [Column("email")]
    public String Email {get;set;} = String.Empty;

    [Required]
    [MaxLength(10)]
    [Column("username")]
    public String Username {get;set;} = String.Empty;

    [Required]
    [MaxLength(12)]
    [Column("password")]
    public String Password {get;set;} = String.Empty;

    [Column("karma")]
    public int Karma {get;set;}
}