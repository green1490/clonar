// int id = Convert.ToInt32(User.Claims.First(x => x.Type == "id").Value);
//         comment.UserID = id;
using System.Formats.Asn1;
using Data;
using Entity;

public static class CommentEndPoint
{
    public static void MapComment(this WebApplication app)
    {
        app.MapPost("comment",async (Comment comment, HttpContext ctx, DataContext db) => 
        {
            int id = Convert.ToInt32(ctx.User.Claims.First(x => x.Type == "id").Value);
            comment.UserID = id;
            await db.AddAsync(comment);
            await db.SaveChangesAsync();
            ctx.Response.StatusCode = StatusCodes.Status200OK;
            await ctx.Response.StartAsync();
        });

        app.MapGet("comment/{id:int}",async (int id, DataContext db, HttpContext ctx) => 
        {
            Comment[] comments = db.Comments.Where(comment => comment.threadID == id).ToArray();
            await ctx.Response.WriteAsJsonAsync(comments);
        });
    }
}