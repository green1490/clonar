using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity;

[Table("comment")]
public class Comment
{
    [Column("id")]
    [SwaggerSchema(ReadOnly = true)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID {get;set;}

    [Column("userid")]
    [ForeignKey("account")]
    [SwaggerSchema(ReadOnly = true)]
    public int UserID {get;set;}

    [DefaultValue(null)]
    [Column("parentid")]
    [ForeignKey("comment")]
    public int? ParentID {get;set;}

    [Required]
    [Column("threadid")]
    public int threadID {get;set;}

    [Required]
    [MaxLength(3000)]
    [Column("usertext")]
    public string UserText {get;set;} = string.Empty;

    [Column("deleted")]
    [DefaultValue(false)]
    [SwaggerSchema(ReadOnly = true)]
    public bool Deleted {get;set;}

    [Column("karma")]
    public int Karma {get;set;}
}