using Schneider.Minefield.Core.Model;
using Xunit;

namespace Schneider.Minefield.Tests.Model
{

    public class LocationTests
    {
        [Fact]
        public void ShouldBeAbleToConvertLocationIntoChessFormat()
        {
            var location = new Location();

            Assert.Equal("A1", location.ChessFormat());
        }

        [Fact]
        public void ShouldBeAbleToCompareLocations()
        {
            var location1 = new Location();
            var location2 = new Location(1, 2);

            Assert.True(location1 != location2);
            Assert.False(location1 == location2);

            location1.X = location2.X;
            location1.Y = location2.Y;

            Assert.True(location1 == location2);
            Assert.False(location1 != location2);

            Assert.True(location1 != null);
            Assert.True(null != location1);

            Assert.False(location1 == null);
            Assert.False(null == location1);
        }
    }
}