using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Schneider.Minefield.Core.Model;
using Xunit;

namespace Schneider.Minefield.Tests.Model
{
   public class PlayerTests
   {
       private Player testPlayer;
       private Location textLocation;

        public PlayerTests()
        {
            textLocation = new Location(5, 5);

            testPlayer = new Player(new Location(textLocation.X,textLocation.Y), "Fred", 3);
        }

        [Fact]
        public void ShouldNotBeAbleToCreateAnInvalidPlayer()
        {
            Assert.Throws<ArgumentException>(() => new Player(null,"fred", 1));
        }

        [Fact]
        public void ShouldBeAbleToMovePlayerUp()
        {
            testPlayer.MoveUp();

            Assert.Equal(textLocation.Y+1,testPlayer.Location.Y);
        }

        [Fact]
        public void ShouldBeAbleToMovePlayerDown()
        {

            testPlayer.MoveDown();

            Assert.Equal(textLocation.Y-1,testPlayer.Location.Y);
        }

        [Fact]
        public void ShouldBeAbleToMovePlayerRight()
        {
            testPlayer.MoveRight();

            Assert.Equal(textLocation.X+1,testPlayer.Location.X);
        }

        [Fact]
        public void ShouldBeAbleToMovePlayerLeft()
        {
            testPlayer.MoveLeft();

            Assert.Equal(textLocation.X-1,testPlayer.Location.X);
        }

        [Fact]
        public void ShouldBeAbleToRemoveLifeFromPlayer()
        {
            int numberOfLives = 3;
            var player = new Player(new Location(5, 5), "Fred", numberOfLives);

            player.DecrementLife();

            Assert.Equal(numberOfLives-1,player.NumberOfLives);
        }

        [Fact]
        public void ShouldBeAbleToDetermineWhetherPlayerHasRemainingLives()
        {
            int numberOfLives = 1;
            var player = new Player(new Location(5, 5), "Fred", numberOfLives);

            Assert.True(player.HasLives);

            player.DecrementLife();

            Assert.False(player.HasLives);
        }
    }
}