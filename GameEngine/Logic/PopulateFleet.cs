using GameEngine.Models;

namespace GameEngine.Logic;

public class PopulateFleet
{
    public static PlayerFleet BuildPlayerFleet(Map map, Player player)
    {
        //var allocateType = player.Name == "P1" ? AllocationType.AllyShip : AllocationType.EnemyShip;
        var allocateType = AllocationType.EnemyShip;
        Console.WriteLine("Bulding " + player.Name + " fleet");
        
        var playerFleet = PopulateShips.RandomLocalize(map, allocateType);
        playerFleet.AssignPlayer(player);
        
        return playerFleet;
    }
}