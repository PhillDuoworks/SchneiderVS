namespace Schneider.Minefield.Core.Utilties;

public interface IKeyboardReader
{
    Action<ConsoleKey> OnPressed { get; set; }

    Task Listen(CancellationToken cancellationToken);
    void RemoveInputListeners();
}