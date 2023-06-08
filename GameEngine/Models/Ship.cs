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
    public Ship(Dictionary<(int, int), bool> position, int length, string shipClass)
    {
        Position = position;
        Length = length;
        ShipClass = shipClass;
    }

    public string ShipClass { get; set; }
    public int Length { get; set; }
    public bool IsSunk
    {
        get { return Position.All(kv => !kv.Value); }
    }
    public Dictionary<(int, int), bool> Position { get; set; }
}