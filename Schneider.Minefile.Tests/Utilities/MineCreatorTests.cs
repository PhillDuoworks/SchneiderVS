using Schneider.Minefield.Core.Model;
using Schneider.Minefield.Core.Utilties;
using Xunit;

namespace Schneider.Minefield.Tests.Utilities
{
   public class MineCreatorTests
   {
        [Fact]
        public void ShouldBeAbleToCreateTenMines()
        {
            var mineCreator = new MineCreator();

            var mines = mineCreator.Generate(10, 10, 10);

            Assert.Equal(10, mines.Count);
        }

        [Fact]
        public void ShouldBeAbleToCreateInSameLocation()
        {
            var mineCreator = new MineCreator();

            var mines = new List<Location> { new Location(1, 2) };

            Assert.True(mineCreator.CheckForExistingMine(mines, new Location(1,2)));
            Assert.False(mineCreator.CheckForExistingMine(mines, new Location(2,2)));
        }
   }
}