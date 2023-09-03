using Schneider.Minefield.Core.Utilties;

namespace Schneider.Minefield.Core.Model;

public class GameBoard : IGameBoard
{
    public int Width { get; }
    public int Height { get; }

    public IList<Location> Mines { get; }

    private IKeyboardReader _keyboardReader;
    private IGameEngine _gameEngine;

    public GameBoard(int width, int height, int numberOfMines, IMineCreator? mineCreator)
    {
        if (width < 0)
        {
            throw new ArgumentException("The width of the play surface must be between 0 and 25");
        }

        if (height < 0)
        {
            throw new ArgumentException("The height of the play surface must be greater than or equal to 0");
        }

        if (mineCreator == null)
        {
            throw new ArgumentException("A mine creator must be provided");
        }

        int playingArea = width * height;

        if (numberOfMines >= playingArea)
        {
            throw new ArgumentException("There are either to many mines or the playing has been completely filled");
        }

        Width = width;
        Height = height;

        Mines = mineCreator.Generate(width, height, numberOfMines);
    }

    public bool IsPlayerOnAMine(IPlayer player)
    {
        return Mines.Contains(player.Location);
    }

    public Location GetStartLocation()
    {
        var rnd = new Random();

        Location startLocation;

        do
        {
            startLocation = new Location(rnd.Next(0, Width), rnd.Next(0, Height));
        } while (Mines.Contains(startLocation));

        return startLocation;
    }

    public void RemoveMine(Location location)
    {
        Mines.Remove(location);
    }
}