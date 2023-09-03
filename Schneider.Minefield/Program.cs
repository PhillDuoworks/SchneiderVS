// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schneider.Minefield.Core;
using Schneider.Minefield.Core.Model;
using Schneider.Minefield.Core.Utilties;
using Serilog;

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
            var logger = new SerilogBuilder().GetLogger(_configuration, "1.0.0.0");

            try
            {
                var builder = new HostBuilder()
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddSingleton<ILogger>(x => logger);
                        services.AddScoped<IGameBoard, GameBoard>();
                        services.AddScoped<IMineCreator, MineCreator>();
                        services.AddScoped<IGameDisplay, GameDisplay>();
                        services.AddScoped<IGameEngine, GameEngine>();
                        services.AddSingleton(_configuration);
                        services.AddSingleton<IGameBoard>(x => new GameBoard(
                            boardWidth,
                            boardHeight,
                            numberOfLives,
                            x.GetService<IMineCreator>()
                        ));

                        services.AddScoped<GameController, GameController>();
                        services.AddHostedService<Mindfield.MinefieldGame>();
                    });

                await builder.RunConsoleAsync();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex,"Unable to start game see exception for further details");
            }
        }
    }
}