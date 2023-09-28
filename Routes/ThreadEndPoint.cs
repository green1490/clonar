namespace MinAPISeparateFile;

using Data;
using Thread = Entity.Thread;

public static class ThreadEndPoint
{
    public static void MapThread(this WebApplication app)
    {
        app.MapGet("thread/{id:int}", async (HttpContext ctx, int id, DataContext db) => {
            Thread? thread =  db.Threads.Find(id);
            if (thread is null)
            {
                ctx.Response.StatusCode = StatusCodes.Status404NotFound;
                await ctx.Response.StartAsync();
            }
            else
            {
                await ctx.Response.WriteAsJsonAsync(thread);
            }
        })
        .WithOpenApi();

        app.MapPost("thread", async (Thread thread, HttpContext ctx, DataContext db) => 
        {
            int collID = db.Collections.AsParallel().First(collection => collection.ColName == thread.CollectionName).ID;
            int id = Convert.ToInt32(ctx.User.Claims.First(x => x.Type == "id").Value);
            thread.CollectionID = collID;
            thread.UserID = id;

            await db.AddAsync(thread);
            await db.SaveChangesAsync();
        })
        .WithOpenApi();
    }
}