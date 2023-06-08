namespace GameEngine.Models;

public class PlayerFleet
{
    public Player Player { get; set; }
    public List<Ship> Ships { get; set; } = new();

    public void AddShip(string shipClass, Ship ship)
    {
        if (Ships.Count >= 5)
        {
            throw new InvalidOperationException("Cannot add more than 5 ships.");
        }

        if (Ships.Any(s => s.ShipClass == shipClass))
        {
            throw new InvalidOperationException($"Cannot add more than one ship of type {shipClass}.");
        }

        Ships.Add(ship);
    }

    public int ShipsLeft()
    {
        return Ships.Count(x => !x.IsSunk);
    }

    public void AssignPlayer(Player player)
    {
        Player = player;
    }
    
    public bool IsFleetSunk()
    {
        return Ships.All(x => x.IsSunk);
    }
    
}