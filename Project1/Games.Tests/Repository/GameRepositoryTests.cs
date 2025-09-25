using FluentAssertions;
using Games.Data;
using Games.Models;
using Games.Repositories;

namespace Games.Tests
{
    public class GameRepositoryTests : TestBase
    {
        private readonly GameRepository _repository;

        public GameRepositoryTests()
        {
            _repository = new GameRepository(Context);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllSeededGames()
        {
            // Arrange
            await SeedDataAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            result.Should().ContainSingle(g => g.Title == "Final Fantasy VI" && g.Developer == "Square");
            result.Should().ContainSingle(g => g.Title == "Pokémon Platinum" && g.Developer == "Game Freak");
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ShouldReturnGameWithPlatforms()
        {
            // Arrange
            await SeedDataAsync();

            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result!.Title.Should().Be("Final Fantasy VI");
            result.Developer.Should().Be("Square");
            result.GamePlatforms.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            await SeedDataAsync();

            // Act
            var result = await _repository.GetByIdAsync(999);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddAsync_ShouldAddGameToDatabase()
        {
            // Arrange
            await SeedDataAsync();

            var newGame = new Game
            {
                Title = "Chrono Trigger",
                Developer = "Square",
                ReleaseYear = 1995
            };

            // Act
            await _repository.AddAsync(newGame);
            await _repository.SaveChangesAsync();

            // Assert
            newGame.GameId.Should().BeGreaterThan(0);

            var gameInDb = await Context.Games.FindAsync(newGame.GameId);
            gameInDb.Should().NotBeNull();
            gameInDb!.Title.Should().Be("Chrono Trigger");
            gameInDb.Developer.Should().Be("Square");
        }
    }
}
