using System.Reflection.Emit;
using Xunit;
using Moq;
using Schneider.Minefield.Core;
using Schneider.Minefield.Core.Model;
using Schneider.Minefield.Core.Utilties;
using Serilog;

namespace Schneider.Minefield.Tests.Core
{

    public class GameEngineTests
    {
        private ILogger _logger;

        public GameEngineTests()
        {
            _logger = new Mock<ILogger>().Object;
        }
        [Fact]
        public void ShouldBeAbleToMovePlayerUp()
        {
            var mockPlayer = new Mock<IPlayer>();

            mockPlayer.Setup(m => m.MoveUp());

            var gameEngine = new GameEngine(_logger);

            gameEngine.HandleInput(ConsoleKey.UpArrow, mockPlayer.Object);

            mockPlayer.Verify(m => m.MoveUp(), Times.Once());
        }

        [Fact]
        public void ShouldBeAbleToMovePlayerRight()
        {
            var mockPlayer = new Mock<IPlayer>();

            mockPlayer.Setup(m => m.MoveRight());

            var gameEngine = new GameEngine(_logger);

            gameEngine.HandleInput(ConsoleKey.RightArrow, mockPlayer.Object);

            mockPlayer.Verify(m => m.MoveRight(), Times.Once());
        }

        [Fact]
        public void ShouldBeAbleToMovePlayerDown()
        {
            var mockPlayer = new Mock<IPlayer>();

            mockPlayer.Setup(m => m.MoveDown());

            var gameEngine = new GameEngine(_logger);

            gameEngine.HandleInput(ConsoleKey.DownArrow, mockPlayer.Object);

            mockPlayer.Verify(m => m.MoveDown(), Times.Once());
        }

        [Fact]
        public void ShouldBeAbleToMovePlayerLeft()
        {
            var mockPlayer = new Mock<IPlayer>();

            mockPlayer.Setup(m => m.MoveLeft());

            var gameEngine = new GameEngine(_logger);

            gameEngine.HandleInput(ConsoleKey.LeftArrow, mockPlayer.Object);

            mockPlayer.Verify(m => m.MoveLeft(), Times.Once());
        }

        [Fact]
        public void ShouldKeepPlayerOnTheBoard()
        {
            int x = -1;
            int y = 6;

            var mockMineGenerator = new Mock<IMineCreator>();
            mockMineGenerator.Setup(m => m.Generate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new List<Location> { new Location(0, 4) });

            var playerLocation = new Location(x, y);
            var gameBoard = new GameBoard(5, 5, 1, mockMineGenerator.Object);

            var player = new Player(playerLocation, "test", 3);

            var gameEngine = new GameEngine(_logger);
            gameEngine.ApplyGameRules(gameBoard, player);

            Assert.Equal(0, playerLocation.X);
            Assert.Equal(5, playerLocation.Y);
        }

        [Fact]
        public void ShouldSetPlayLocationMessagePlayer()
        {
            int x = 0;
            int y = 4;

            var mockMineGenerator = new Mock<IMineCreator>();
            mockMineGenerator.Setup(m => m.Generate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new List<Location> { new Location(0, 4) });

            var playerLocation = new Location(x, y);
            var gameBoard = new GameBoard(5, 5, 1, mockMineGenerator.Object);

            var player = new Player(playerLocation, "test", 3);

            var gameEngine = new GameEngine(_logger);
            gameEngine.ApplyGameRules(gameBoard, player);

            Assert.Contains(string.Format(GameMessages.PlayerLocation, "A5"), gameEngine.StatusMessage);

        }

        [Fact]
        public void ShouldDetectPlayerIsOnAMine()
        {
            int x = -1;
            int y = 6;

            var mockMineGenerator = new Mock<IMineCreator>();
            mockMineGenerator.Setup(m => m.Generate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new List<Location> { new Location(0, 5) });

            var playerLocation = new Location(x, y);
            var gameBoard = new GameBoard(5, 5, 1, mockMineGenerator.Object);

            var player = new Player(playerLocation, "test", 3);

            var gameEngine = new GameEngine(_logger);
            gameEngine.ApplyGameRules(gameBoard, player);

            Assert.Equal(2, player.NumberOfLives);
            Assert.Empty(gameBoard.Mines);
        }

        [Fact]
        public void ShouldDetectPlayerWin()
        {
            int boardWidth = 5;
            int boardHeight = 5;

            int x = 6;
            int y = 4;

            var mockMineGenerator = new Mock<IMineCreator>();
            mockMineGenerator.Setup(m => m.Generate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new List<Location> { new Location(0, 5) });

            var playerLocation = new Location(x, y);
            var gameBoard = new GameBoard(boardWidth, boardHeight, 1, mockMineGenerator.Object);

            var player = new Player(playerLocation, "test", 3);

            var gameEngine = new GameEngine(_logger);
            gameEngine.ApplyGameRules(gameBoard, player);

            Assert.True(player.HasLives);
            Assert.Contains(GameMessages.Winner, gameEngine.StatusMessage);
        }
    }
}