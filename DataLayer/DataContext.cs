#nullable disable
using Microsoft.EntityFrameworkCore;
using Entity;

namespace Data;

public class DataContext: DbContext
{
    public DbSet<Entity.Thread> Threads {get;set;}
    public DbSet<Account> Accounts {get;set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseNpgsql(@"Host=localhost;Username=root;Password=root;Database=root");

}