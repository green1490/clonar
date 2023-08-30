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

    [Required]
    [MaxLength(100)]
    [Column("title")]
    public string Title {get;set;} = string.Empty;

    [Required]
    [MaxLength(3000)]
    [Column("threadtext")]
    public string ThreadText {get;set;} = string.Empty;

    [Column("collectionid")]
    [ForeignKey("collection")]
    [SwaggerSchema(ReadOnly = true)]
    public int CollectionID {get;set;}

    [Required]
    [NotMapped]
    public string CollectionName {get;set;} = string.Empty;

    [Column("deleted")]
    [DefaultValue(false)]
    [SwaggerSchema(ReadOnly = true)]
    public bool Deleted {get;set;}

    [Column("karma")]
    public int Karma {get;set;}
}