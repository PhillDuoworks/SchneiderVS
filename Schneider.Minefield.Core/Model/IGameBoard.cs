namespace Schneider.Minefield.Core.Model;

public interface IGameBoard
{
    int Width { get;}
    int Height { get;}

    bool IsPlayerOnAMine(IPlayer player);
    bool HasPlayerEscapedMinefield(IPlayer player);
    Location GetStartLocation();

}