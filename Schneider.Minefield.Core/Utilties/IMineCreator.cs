using Schneider.Minefield.Core.Model;

namespace Schneider.Minefield.Core.Utilties;

public interface IMineCreator
{
    IList<Location> Generate(int width, int height, int numberOfMines);
}