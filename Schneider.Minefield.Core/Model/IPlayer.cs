using System.Data.Common;

namespace Schneider.Minefield.Core.Model;

public interface IPlayer
{
    public Location Location { get; }
    public string Name { get; set; }
    public int NumberOfLives { get; }

    public void DecrementLife();
    public bool HasLives { get; }

    public void MoveUp();
    public void MoveRight();
    public void MoveDown();
    public void MoveLeft();
}