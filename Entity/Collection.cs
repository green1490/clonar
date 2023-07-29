using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity;

//contais threads with a specific interest
[Table("collection")]
public class Collection
{
    [Column("id")]
    [SwaggerSchema(ReadOnly = true)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID {get;set;}

    [MaxLength(30)]
    [Column("colname")]
    public string ColName {get;set;} = string.Empty;

    [MaxLength(200)]
    [Column("description")]
    public string Description {get;set;} = string.Empty;
}