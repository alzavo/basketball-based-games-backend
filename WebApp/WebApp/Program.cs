using DAL.App.EF;
using DAL.App.EF.DataInit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
     options
         .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
         .EnableDetailedErrors()
         .EnableSensitiveDataLogging()
     );

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/////////////

using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
using var ctx = serviceScope.ServiceProvider.GetService<AppDbContext>();
if (ctx != null) 
{
    DataInit.SeedGamesData(ctx);
}

/////////////


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
