using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity;

[Table("thread")]
public class Thread
{
    [Key]
    [Column("id")]
    [SwaggerSchema(ReadOnly = true)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID {get;set;}

    [Column("userid")]
    [ForeignKey("account")]
    [SwaggerSchema(ReadOnly = true)]
    public int UserID {get;set;}

    [Column("title")]
    [MaxLength(100)]
    public string Title {get;set;} = string.Empty;

    [Column("threadtext")]
    [MaxLength(3000)]
    public string ThreadText {get;set;} = string.Empty;

    [Column("collectionid")]
    [ForeignKey("collection")]
    public int CollectionID {get;set;}

    [Column("deleted")]
    [DefaultValue(false)]
    public bool Deleted {get;set;}
}