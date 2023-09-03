
using System.Text;
using Schneider.Minefield.Core.Model;
using Serilog;

namespace Schneider.Minefield.Core.Utilties;

public class GameEngine : IGameEngine
{
    private ILogger _logger;
    public string StatusMessage => _gameStatus.ToString();

    private StringBuilder _gameStatus;

    public GameEngine(ILogger logger)
    {
        _logger = logger;
        _gameStatus = new StringBuilder("Game started");
    }
    public void HandleInput(ConsoleKey key, IPlayer player)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                player.MoveUp();
                break;
            case ConsoleKey.RightArrow:
                player.MoveRight();
                break;
            case ConsoleKey.DownArrow:
                player.MoveDown();
                break;
            case ConsoleKey.LeftArrow:
                player.MoveLeft();
                break;

            default:
                _logger.Debug($"Key {key} not recognosed");
                break;
        }
    }

    public void ApplyGameRules(IGameBoard gameBoard, IPlayer player)
    {
        _gameStatus = new StringBuilder();

        CheckPlayer(gameBoard, player);

        _gameStatus.AppendFormat(GameMessages.PlayerLocation, player.Location.ChessFormat());

        if (CheckForWin(gameBoard, player))
        {
            _gameStatus.Append(GameMessages.Winner);
        }
    }

    private bool CheckForWin(IGameBoard gameBoard, IPlayer player)
    {
        return player.HasLives && player.Location.X > gameBoard.Width;
    }

    private void CheckPlayer(IGameBoard gameBoard, IPlayer player)
    {
        // keep player on the board
        if (player.Location.X < 0)
        {
            player.Location.X = 0;
        }

        if (player.Location.Y < 0)
        {
            player.Location.Y = 0;
        }

        if (player.Location.Y > gameBoard.Height)
        {
            player.Location.Y = gameBoard.Height;
        }

        //Check for mine
        var steppedOnMine = gameBoard.IsPlayerOnAMine(player);

        if (steppedOnMine)
        {
            player.DecrementLife();
            gameBoard.RemoveMine(player.Location);
        }
    }
}