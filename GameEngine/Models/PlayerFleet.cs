namespace GameEngine.Models;

public class PlayerFleet
{
    public Player Player;
    public List<Ship> Ships;

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

    public void SunkShipByLastHittedPosition(int[,] position)
    {
        var ship = Ships.Find(x => x.Position.Contains(position));
        if (ship != null) ship.IsSunk = true;
    }

    public bool IsFleetSunk()
    {
        return Ships.All(x => x.IsSunk);
    }
    
}