using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Swashbuckle.AspNetCore.Annotations;

namespace Entity;

[Table("account")]
public class Account
{
    [Key]
    [Column("id")]
    [SwaggerSchema(ReadOnly = true)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
}