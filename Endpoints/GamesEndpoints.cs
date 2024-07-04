using GameStroreApi.Data;
using GameStroreApi.Dtos;
using GameStroreApi.Mapping;
using GameStroreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStroreApi.Endpoints;

public static class GamesEndpoints
{

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
                            .WithParameterValidation();

        // GET /games
        const string GetGameEndpointName = "GetGame";

        group.MapGet("/", async (GameStoreContext dbContext) =>

            await dbContext.Games
                .Include(game => game.Genre)
                .Select(game => game.ToGameSummaryDto())
                .AsNoTracking()
                .ToListAsync()
        );

        //GET /games/1

        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        }
        ).WithName(GetGameEndpointName);

        // POST /games

        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();


            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
        });

        // PUT /games/1

        group.MapPut("/{id}", async (int id, UpdateGameDto updateGame, GameStoreContext dbContext) =>
        {

            // var index = games.FindIndex(game => game.Id == id);
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
            .CurrentValues
            .SetValues(updateGame.ToEntity(id));

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        }).WithParameterValidation(); ;

        // DELETE /games/1

        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }

}
