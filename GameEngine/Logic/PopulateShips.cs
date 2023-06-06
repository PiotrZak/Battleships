using GameEngine.Models;

namespace GameEngine.Logic;

public class PopulateShips
{

    public void BuildShips()
    {

        var playerFleet = new PlayerFleet();
        
        playerFleet.AddShip(ShipClass.Destroyer, new Destroyer());
        playerFleet.AddShip(ShipClass.Submarine, new Submarine());
        playerFleet.AddShip(ShipClass.Cruiser, new Cruiser());
        playerFleet.AddShip(ShipClass.Battleship, new Battleship());
        playerFleet.AddShip(ShipClass.Carrier, new Carrier());
        
    }

    // public List<int[,]> AllocateShip()
    // {
    //     
    // }
}