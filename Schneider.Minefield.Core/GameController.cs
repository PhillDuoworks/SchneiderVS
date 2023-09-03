using Schneider.Minefield.Core.Model;
using Schneider.Minefield.Core.Utilties;
using Serilog;

namespace Schneider.Minefield.Core;

public class GameController : IGameController
{
    private readonly Player _player;
    private readonly IGameBoard _gameBoard;
    private readonly IKeyboardReader _keyboardReader;
    private readonly IGameEngine _gameEngine;
    private readonly IGameDisplay _gameDisplay;
    private readonly ILogger _logger;

    public GameController(IGameBoard gameBoard, IKeyboardReader keyboardReader, IGameEngine gameEngine, IGameDisplay gameDisplay, ILogger logger, int numberOfLives)
    {
        _player = new Player(gameBoard.GetStartLocation(), "Fred", numberOfLives);
        _gameBoard = gameBoard;
        _keyboardReader = keyboardReader;
        _gameEngine = gameEngine;
        _gameDisplay = gameDisplay;
        _logger = logger;
    }

    public async Task Run(CancellationToken cancellationToken)
    {
        _logger.Debug("Registering keypress handler");

        try
        {
            _keyboardReader.OnPressed += (key) =>
            {
                _gameEngine.HandleInput(key, _player);
                _gameEngine.ApplyGameRules(_gameBoard, _player);
                _gameDisplay.Update(_gameEngine.StatusMessage);
            };

            await _keyboardReader.Listen(cancellationToken);

        }
        catch (Exception e)
        {
            _logger.Fatal(e,"Unrecoverable error has occured see exception for further details");
        }
    }
}