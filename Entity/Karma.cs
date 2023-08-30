using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity;

[Keyless]
[Table("karma")]
public class Karma
{
    [Column("userID")]
    [ForeignKey("account")]
    public int UserID {get;init;}

    [Column("threadKarma")]
    [ForeignKey("thread")]
    public int ThreadKarma {set;get;}

    [ForeignKey("comment")]
    [Column("commentKarma")]
    public int Commentkarma {set;get;}
}
