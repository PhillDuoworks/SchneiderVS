using Schneider.Minefield.Core.Model;

namespace Schneider.Minefield.Core.Utilties;

public interface IStateHandler
{
    void HandleInput(ConsoleKey key, Player player);
    void UpdateGameState(IGameBoard gameBoard, Player player);
}