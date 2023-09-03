using Schneider.Minefield.Core.Model;

namespace Schneider.Minefield.Core.Utilties;

public interface IGameEngine
{
    void HandleInput(ConsoleKey key, IPlayer player);
    void ApplyGameRules(IGameBoard gameBoard, IPlayer player);
    string StatusMessage { get;  }
}