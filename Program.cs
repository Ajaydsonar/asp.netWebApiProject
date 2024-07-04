using GameStroreApi;
using GameStroreApi.Data;
using GameStroreApi.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");

// builder.Services.AddDbContext<GameStoreContext>(options =>
//     options.UseSqlite(connectionString)
// );

builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();

app.MapGamesEndpoints();
app.MapGenresEndpoints();

await app.MigrateDbAsync();

app.Run();
