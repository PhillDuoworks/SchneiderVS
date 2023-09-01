namespace Schneider.Minefield.Core.Model;

public class Player : IPlayer
{
    public Location Location { get; }
    public string Name { get; set; }

    public int NumberOfLives { get; private set; }

    public Player(Location location, string name, int numberOfLives)
    {
        if (location == null)
        {
            throw new ArgumentException("Location can not be null");
        }
        
        Location = location;
        Name = name;
        NumberOfLives = numberOfLives;
    }

    public void DecrementLife()
    {
        NumberOfLives--;
    }

    public bool HasLives => NumberOfLives > 0;

    public void MoveUp()
    {
        Location.Y++;
    }

    public void MoveRight()
    {
        Location.X++;
    }

    public void MoveDown()
    {
        Location.Y--;
    }

    public void MoveLeft()
    {
        Location.X--;
    }
}