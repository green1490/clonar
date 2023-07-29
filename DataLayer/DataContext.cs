#nullable disable
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class DataContext: DbContext
{
    public DbSet<Entity.Thread> Threads {get;set;}
    public DbSet<Account> Accounts {get;set;}
    public DbSet<Collection> Collections {get;set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseNpgsql(@"Host=localhost;Username=root;Password=root;Database=root");

}