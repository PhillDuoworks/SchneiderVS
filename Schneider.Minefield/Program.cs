// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schneider.Minefield.Core;
using Schneider.Minefield.Core.Model;
using Schneider.Minefield.Core.Utilties;

namespace Schneider.Minefield
{
    internal class Program
    {
        private static readonly IConfiguration _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        public static async Task Main()
        {
            var numberOfMines = _configuration.GetValue<int>("numberOfMines");
            var boardWidth = _configuration.GetValue<int>("boardWidth");
            var boardHeight = _configuration.GetValue<int>("boardHeight");
            var numberOfLives = _configuration.GetValue<int>("numberOfLives");

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<IGameBoard, GameBoard>();
                    services.AddScoped<IMineCreator, MineCreator>();
                    services.AddSingleton(_configuration);
                    services.AddSingleton<IGameBoard>(x => new GameBoard(
                        boardWidth,
                        boardHeight,
                        numberOfLives,
                        x.GetService <IMineCreator>()
                        ));

                    services.AddScoped<GameController, GameController>();
                    services.AddHostedService<Mindfield.MinefieldGame>();
                });

            await builder.RunConsoleAsync();
        }
    }
}