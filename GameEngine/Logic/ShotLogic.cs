using GameEngine.Models;

namespace GameEngine.Logic;

public abstract class ShotLogic
{
    public static void Shot(PlayerFleet playerFleet, Map map, GameMode mode, int round)
    {
        while (true)
        {
            Console.WriteLine("Where to shot? (eg A5)");
            
            var userShotCoordinates = mode switch
            {
                GameMode.AvA => AIChoose(),
                GameMode.PvA => round % 2 != 0 ? Console.ReadLine() : AIChoose(),
                GameMode.PvP => Console.ReadLine(),
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            };

            if (Validators.IsInputCorrect(userShotCoordinates))
            {
                var (x, y) = Parsers.ParseUserInput(userShotCoordinates);

                if (map.Coordinates.TryGetValue((x, y), out var allocationType))
                {
                    switch (allocationType)
                    {
                        case AllocationType.AllyShip:
                            Console.WriteLine("Cannot attack allies!");
                            continue;
                        case AllocationType.ShotMissed:
                            Console.WriteLine("Hey its already missed, don't waste energy and resources!");
                            continue;
                        case AllocationType.AllyShipHitted:
                            Console.WriteLine("Its your ship already attacked!");
                            continue;
                        case AllocationType.EnemyHitted:
                            Console.WriteLine("Its enemy ship already attacked");
                            continue;
                        case AllocationType.Water:
                        case AllocationType.EnemyShip:
                        default:
                            HandleHit(playerFleet, map, x, y);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Key not found - Out Of board");
                }
            }
            else
            {
                continue;
            }

            break;
        }
    }

    public static string? AIChoose()
    {
        Random random = new Random();
        int columnIndex = random.Next(10);
        char column = (char)('A' + columnIndex);
        int row = random.Next(1, 11);
        return $"{column}{row}";
    }

    public static void HandleHit(PlayerFleet playerFleet, Map map, int x, int y)
    {
        var hitted = IsHitted(map, (x, y));
        if (hitted)
        {
            var ship = playerFleet.Ships.SingleOrDefault(ship => ship.Position.ContainsKey((x, y)));

            if (ship == null)
            {
                throw new Exception("Opposite fleet passes - there isn't ship of this fleet on this coordinate");
            }

            ship.Position[(x, y)] = false;
            if (ship.IsSunk)
            {
                Console.WriteLine("Congrats! enemy ship " + ship.ShipClass + " sunked!");
                Console.WriteLine("Left Enemy ships " + playerFleet.ShipsLeft());
            }
            else
            {
                Console.WriteLine("Enemy Ship " + ship.ShipClass + " hitted - left ship fields: " + ship.Position.Count(isHitted => isHitted.Value));
            }
        }
    }
    
    public static bool IsHitted(Map map, (int, int) coordinates)
    {
        if (map.Coordinates[coordinates] == AllocationType.EnemyShip)
        {
            map.Coordinates[coordinates] = AllocationType.EnemyHitted;
            return true;
        }
        Console.WriteLine("Miss");
        map.Coordinates[coordinates] = AllocationType.ShotMissed;

        return false;
    }
}