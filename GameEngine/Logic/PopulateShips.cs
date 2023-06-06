using GameEngine.Models;

namespace GameEngine.Logic;

public class PopulateShips
{

    public static void BuildShips(Map map)
    {

        var playerFleet = new PlayerFleet();

        RandomLocalize(map);
        
        // playerFleet.AddShip(ShipClass.Destroyer, new Destroyer());
        // playerFleet.AddShip(ShipClass.Submarine, new Submarine());
        // playerFleet.AddShip(ShipClass.Cruiser, new Cruiser());
        // playerFleet.AddShip(ShipClass.Battleship, new Battleship());
        // playerFleet.AddShip(ShipClass.Carrier, new Carrier());
        
    }

    private static void RandomLocalize(Map map)
    {
        
        //randomly generate coords
        var destroyerCoords = new List<(int, int)>()
        {
            (0, 6),
            (0, 7)
        };
        
        // check if not occupied by ship
        
        
        var destroyer = new Ship(destroyerCoords, ShipClass.Destroyer.ToString());
        
        // place on map
    }
    
    
    
    // 012345678910
    // A~~~~~~~~~~
    // B~~~~~~~~~~
    // C~~~~~~~~~~
    // D~~~~~~~~~~
    // E~~~~~~~~~~
    // F~~~~~~~~~~
    // G~~~~~~~~~~
    // H~~~~~~~~~~
    // I~~~~~~~~~~
    // J~~~~~~~~~~

}