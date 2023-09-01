namespace Schneider.Minefield.Core.Model;

public class Location
{
    public int X { get; set; }
    public int Y { get; set; }

    public Location()
    {
        X = 0;
        Y = 0;
    }

    public Location(int x, int y)
    {
        X = x;
        Y = y;
    }

    public string ChessFormat()
    {
        var letter = ('A' + X).ToString();

        return $"{((char) ('A' + X))}{Y + 1}";
    }

    public static bool operator ==(Location x, Location y) {

        if (x is null )
        {
            return y is null;
        }
        return   x.Equals(y);
    }

    public static bool operator !=(Location x, Location y)
    {

        return !(x == y);
    }

    public override bool Equals(object? obj)
    {
        return obj is Location y && y.X == X && y.Y == Y;
    }

    protected bool Equals(Location other)
    {
        if (other is null)
        {
            return false;
        }
        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}