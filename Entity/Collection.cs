using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Entity;

public class Collection
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int id {get;set;}

    [ForeignKey("Account")]
    public int userID {get;set;}

    [MinLength(10)]
    public string comment {get;set;} = string.Empty;

    [DefaultValue(false)]
    public bool deleted {get;set;}
}