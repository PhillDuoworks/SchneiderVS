namespace Schneider.Minefield.Core;

public interface IGameController
{
    Task Run(CancellationToken cancellationToken);
}