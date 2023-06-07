namespace GameEngine.Models;

public class PlayerFleet
{
    public Player Player { get; set; }
    private List<Ship> Ships { get; set; } = new();

    public void AddShip(ShipClass shipClass, Ship ship)
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

    public void AssignPlayer(Player player)
    {
        Player = player;
    }
    
    public bool IsFleetSunk()
    {
        return Ships.All(x => x.IsSunk);
    }
    
}