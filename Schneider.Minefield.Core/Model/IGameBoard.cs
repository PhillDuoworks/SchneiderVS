namespace Schneider.Minefield.Core.Model;

public interface IGameBoard
{
    int Width { get;}
    int Height { get;}

    bool IsPlayerOnAMine(IPlayer player);
    Location GetStartLocation();

    void RemoveMine(Location playerLocation);
}