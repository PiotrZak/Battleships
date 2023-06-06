namespace GameEngine.Models;

public enum ShipClass
{
    Destroyer,
    Submarine,
    Cruiser,
    Battleship,
    Carrier,
}

public class Ship
{
    public ShipClass ShipClass { get; set; }
    public int Length { get; set; }
    public bool IsSunk { get; set; }
    
    public List<int[,]> Position { get; set; }
}

public class Destroyer : Ship
{
    public Destroyer()
    {
        Length = 2;
    }
}

public class Submarine : Ship
{
    public Submarine()
    {
        Length = 3;
    }
}

public class Cruiser : Ship
{
    public Cruiser()
    {
        Length = 3;
    }
}

public class Battleship : Ship
{
    public Battleship()
    {
        Length = 4;
    }
}

public class Carrier : Ship
{
    public Carrier()
    {
        Length = 5;
    }
}