#nullable disable
using Microsoft.EntityFrameworkCore;
using Entity;

namespace Data;

public class UserContext: DbContext
{
    public DbSet<Account> Accounts {get;set;}
    public DbSet<Collection> Collections {get;set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseNpgsql(@"Host=localhost;Username=root;Password=root;Database=root");

}