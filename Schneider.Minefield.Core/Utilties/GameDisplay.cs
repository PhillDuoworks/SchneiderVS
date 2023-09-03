namespace Schneider.Minefield.Core.Utilties;

public class GameDisplay : IGameDisplay
{
    public void Update(string message)
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(message);
    }
}