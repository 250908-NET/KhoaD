using FluentAssertions;
using Moq;
using Games.Models;
using Games.Repositories;
using Games.Services;

namespace Games.Tests.Services
{
    public class GameServiceTests
    {
        private readonly Mock<IGameRepository> _mockRepository;
        private readonly GameService _service;

        public GameServiceTests()
        {
            _mockRepository = new Mock<IGameRepository>();
            _service = new GameService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllGames()
        {
            // Arrange
            var expectedGames = new List<Game>
            {
                new Game { GameId = 1, Title = "Spider-Man", Developer = "Insomniac", ReleaseYear = 2020 },
                new Game { GameId = 2, Title = "Halo Infinite", Developer = "343 Industries", ReleaseYear = 2021 }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                           .ReturnsAsync(expectedGames);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(expectedGames);

            _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ShouldReturnGame()
        {
            // Arrange
            var expectedGame = new Game
            {
                GameId = 1,
                Title = "Spider-Man",
                Developer = "Insomniac",
                ReleaseYear = 2020
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(expectedGame);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedGame);
            _mockRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(999))
                           .ReturnsAsync((Game?)null);

            // Act
            var result = await _service.GetByIdAsync(999);

            // Assert
            result.Should().BeNull();
            _mockRepository.Verify(repo => repo.GetByIdAsync(999), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddGame()
        {
            // Arrange
            var inputGame = new Game { Title = "Elden Ring", Developer = "FromSoftware", ReleaseYear = 2022 };

            _mockRepository.Setup(repo => repo.AddAsync(inputGame))
                           .Returns(Task.CompletedTask);

            // Act
            await _service.CreateAsync(inputGame);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(inputGame), Times.Once);
            _mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void Constructor_WithNullRepository_ShouldThrow()
        {
            // Act
            Action act = () => new GameService(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
