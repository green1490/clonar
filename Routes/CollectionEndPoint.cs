using System.Diagnostics;
using System.Text.Json;
using Data;
using Entity;

namespace MinAPISeparateFile;

public static class CollectionEndPoint
{
    public static void MapCollection(this WebApplication app)
    {
        app.MapGet("collection/{name}", async (string name, HttpContext ctx, DataContext db) => 
        {
            Collection collection = db.Collections.First(x => x.ColName == name);
            Entity.Thread[] threads = db.Threads.AsParallel().Where(thread => thread.CollectionID == collection.ID).ToArray();
            await ctx.Response.WriteAsJsonAsync(threads);
        })
        .WithOpenApi();

        app.MapPost("collection",async (Collection collection, DataContext db, HttpContext ctx) => 
        {
            int id = Convert.ToInt32(ctx.User.Claims.First(x => x.Type == "id").Value);
            collection.OwnerID = id;
            try
            {
                await db.AddAsync(collection);
                await db.SaveChangesAsync();
            }
            catch
            {
                Debug.Print(JsonSerializer.Serialize(collection));
            }
        })
        .WithOpenApi();
    }
}