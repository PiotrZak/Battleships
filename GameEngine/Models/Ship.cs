namespace GameEngine.Models;

public enum ShipClass
{
    Destroyer = 2,
    Submarine = 3,
    Cruiser = 3,
    Battleship = 4,
    Carrier = 5,
}

public class Ship
{
    public Ship(List<(int, int)> position, string length)
    {
        Position = position;
        Length = Length;
    }

    public ShipClass ShipClass { get; set; }
    public int Length { get; set; }
    public bool IsSunk { get; set; }
    public List<(int, int)> Position { get; set; }
}