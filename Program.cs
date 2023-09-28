using Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MinAPISeparateFile;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.EnableAnnotations();
    }
);

builder.Services.AddDbContext<DataContext>(option => 
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("api"));
});

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme
    )
    .AddCookie(options => 
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(3);
    });

builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder => 
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();

app.MapControllers();

//endpoints
app.MapCollection();
app.MapThread();
app.MapUser();

app.Run();
