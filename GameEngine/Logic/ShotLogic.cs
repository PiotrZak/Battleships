using GameEngine.Models;

namespace GameEngine.Logic;

public abstract class ShotLogic
{
    public static string Shot(PlayerFleet playerFleet, Map map)
    {
            
        Console.WriteLine("Where to shot? (eg A5)");
        var userShotCoordinates = Console.ReadLine();

        if(Validators.IsInputCorrect(userShotCoordinates))
        {
            var (x, y) = Parsers.ParseUserInput(userShotCoordinates);
            
            if (map.Coordinates.TryGetValue((x, y), out var allocationType))
            {
                var hitted = IsHitted(map, (x, y));
                if (hitted)
                {
                    var ship = playerFleet.Ships.SingleOrDefault(ship => ship.Position.ContainsKey((x,y)));
                    
                    if (ship == null)
                    {
                        throw new Exception("implementation goes wrong :(");
                    }
                    ship.Position[(x, y)] = false;
                    if (ship.IsSunk)
                    {
                        Console.WriteLine("Grats! enemy ship " + ship.ShipClass + " sunked!");
                        Console.WriteLine("Left Enemy ships " + playerFleet.ShipsLeft());
                    }
                    else
                    {

                        Console.WriteLine("Enemy Ship " + ship.ShipClass + " lIeft ship fields: " + ship.Position.Count(isHitted => isHitted.Value));
                    }
                }
                
                if (allocationType == AllocationType.AllyShip)
                {
                    Console.WriteLine("Cannot attack allies!");
                    Shot(playerFleet, map);
                }
            }
            else
            {
                Console.WriteLine("Key not found - Out Of board");
            }
        }
        else
        {
            Shot(playerFleet, map);
        }

        return "OK";
    }

    private static bool IsHitted(Map map, (int, int) coordinates)
    {
        if (map.Coordinates[coordinates] == AllocationType.EnemyShip)
        {
            map.Coordinates[coordinates] = AllocationType.EnemyHitted;
            return true;
        }

        if (map.Coordinates[coordinates] != AllocationType.EnemyHitted || map.Coordinates[coordinates] != AllocationType.AllyShipHitted )
        {
            map.Coordinates[coordinates] = AllocationType.ShotMissed;
            Console.WriteLine("Miss");
        }
        
    return false;
}

    private static void ReportShipDamage(Ship ship)
    {
        
    }
    
}