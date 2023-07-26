using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Entity;

[Table("thread")]
public class Thread
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int ID {get;set;}

    [Column("userid")]
    [ForeignKey("account")]
    public int UserID {get;set;}

    [Column("title")]
    [MaxLength(100)]
    public string Title {get;set;} = string.Empty;

    [Column("threadtext")]
    [MaxLength(3000)]
    public string ThreadText {get;set;} = string.Empty;

    [Column("deleted")]
    [DefaultValue(false)]
    public bool Deleted {get;set;}
}