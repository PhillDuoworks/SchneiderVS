namespace Schneider.Minefield.Core.Utilties;

public class KeyBoardReader : IKeyboardReader
{
    public Action<ConsoleKey> OnPressed { get; set; }

    public async Task Listen(CancellationToken cancellationToken)
    {
        await Task.Run(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                while (!Console.KeyAvailable)
                {
                    await Task.Delay(50, cancellationToken);
                }

                OnPressed(Console.ReadKey(true).Key);
            }
        }, cancellationToken);
    }

    public void RemoveInputListeners()
    {
        OnPressed = (key) => { };
    }
}