#nullable disable
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class DataContext: DbContext
{

    public DbSet<Entity.Thread> Threads     {get;set;}
    public DbSet<Account>       Accounts    {get;set;}
    public DbSet<Collection>    Collections {get;set;}
    public DbSet<Comment>       Comments    {get;set;}
    public DbSet<Karma>         Karmas      {get;set;}

    public DataContext(DbContextOptions<DataContext> options): base(options) {}
}