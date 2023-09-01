using Microsoft.Extensions.Hosting;
using Schneider.Minefield.Core;

namespace Schneider.Minefield;

public class Mindfield
{
    public class MinefieldGame : IHostedService
    {
        private readonly IGameController _gameController;

        public MinefieldGame(IGameController gameController)
        {
            _gameController = gameController;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _gameController.Run(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}