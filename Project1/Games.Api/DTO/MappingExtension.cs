using Games.Models;

namespace Games.DTOs;

public static class MappingExtensions
{
    public static GameDto ToDto(this Game game)
    {
        return new GameDto
        {
            GameId = game.GameId,
            Title = game.Title,
            Developer = game.Developer,
            ReleaseYear = game.ReleaseYear,
            Platforms = game.GamePlatforms.Select(gp => gp.Platform.Name).ToList()
        };
    }

    public static PlatformDto ToDto(this Platform platform)
    {
        return new PlatformDto
        {
            PlatformId = platform.PlatformId,
            Name = platform.Name,
            Manufacturer = platform.Manufacturer,
            ReleaseYear = platform.ReleaseYear,
            Games = platform.GamePlatforms.Select(gp => gp.Game.Title).ToList()
        };
    }
}