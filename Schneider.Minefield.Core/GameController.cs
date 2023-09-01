using Schneider.Minefield.Core.Model;
using Schneider.Minefield.Core.Utilties;

namespace Schneider.Minefield.Core;

public class GameController : IGameController
{
    private Player _player;
    private IGameBoard _gameBoard;
    private IKeyboardReader _keyboardReader;
    private IStateHandler _stateHandler;

    public GameController(IGameBoard gameBoard, IKeyboardReader keyboardReader, IStateHandler stateHandler, int numberOfLives)
    {
        _player = new Player(gameBoard.GetStartLocation(), "Fred", numberOfLives);
        _gameBoard = gameBoard;
        _keyboardReader = keyboardReader;
        _stateHandler = stateHandler;
    }

    public async Task Run(CancellationToken cancellationToken)
    {
        _keyboardReader.OnPressed += (key) =>
        {
            _stateHandler.HandleInput(key, _player);
            _stateHandler.UpdateGameState(_gameBoard, _player);
        };

        await _keyboardReader.Listen(cancellationToken);
    }
}