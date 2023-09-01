using Schneider.Minefield.Core.Model;

namespace Schneider.Minefield.Core.Utilties;

public class MineCreator : IMineCreator
{
    public IList<Location> Generate(int width, int height, int numberOfMines)
    {
        var result = new List<Location>();

        Random rnd = new Random();

        for (int i = 0; i < numberOfMines; )
        {
            var newMine = new Location(rnd.Next(0, width), rnd.Next(0, height));

            if (!CheckForExistingMine(result,newMine))
            {
                result.Add(newMine);
                i++;
            }
        }

        return result;
    }

    public bool CheckForExistingMine(IList<Location> mines, Location mine)
    {
        return mines.Contains(mine);
    }
}