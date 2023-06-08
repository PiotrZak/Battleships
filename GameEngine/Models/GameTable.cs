using GameEngine.Logic;

namespace GameEngine.Models;

public enum GameMode
{
    PvP,
    PvA,
    AvA,
}

public class GameTable
{
    private readonly Map map;
    private readonly List<PlayerFleet> fleets;

    public GameTable()
    {
        map = Map.GenerateMap();
        var p1 = new Player( "P1", false);
        var p2 = new Player("P2", false);
        fleets = BuildPlayersFleets(map, p1, p2);
    }

    public void Start()
    {
        Console.WriteLine("Welcome to the Game!");
        Console.WriteLine("Please select a game mode:");
        Console.WriteLine("1. PvP");
        Console.WriteLine("2. Player vs AI");
        Console.WriteLine("3. AI vs AI");

        string input = Console.ReadLine();
        
        switch (input)
        {
            case "1":
                Console.WriteLine("You selected 2 Players mode.");
                PlayGame(GameMode.PvP);
                break;
                
            case "2":
                Console.WriteLine("You selected 1 Player vs AI mode.");
                PlayGame(GameMode.PvA);
                break;
            case "3":
                Console.WriteLine("You selected AI vs AI mode.");
                PlayGame(GameMode.AvA);
                break;
            default:
                Console.WriteLine("Invalid input. Please try again.");
                break;
        }
    }

    private static List<PlayerFleet> BuildPlayersFleets(Map map, Player p1, Player p2)
    {
        var p1Fleet = PopulateFleet.BuildPlayerFleet(map, p1);
        var p2Fleet = PopulateFleet.BuildPlayerFleet(map, p2);
            
        return new List<PlayerFleet> { p1Fleet, p2Fleet };
    }

    private void PlayGame(GameMode mode)
    {
        var currentPlayerIndex = 0;
        var round = 0;
        
        while (!fleets.Any(x => x.IsFleetSunk()))
        {
            var currentPlayerFleet = fleets[currentPlayerIndex];
            var oppositePlayerFleet = fleets.Find(x => x.Player.Name != currentPlayerFleet.Player.Name);
                
            map.DrawMap(false);
            Console.WriteLine("Player " + currentPlayerFleet.Player.Name + " turn.");
            ShotLogic.Shot(oppositePlayerFleet, map, mode, round);
            
            ReverseEnemyContext(map);
            currentPlayerIndex = (currentPlayerIndex + 1) % fleets.Count; // Switch to the next player
            round ++;
        }

        Console.WriteLine("Game Over, Rounds: " + round);
        Console.WriteLine("---------");
        Console.WriteLine(fleets.Find(x => x.IsFleetSunk())?.Player.Name);
        Console.WriteLine("Lost");
    }
    
    //if p1 then p2 is enemy, and if p2 then p1 is enemy
    private static void ReverseEnemyContext(Map map)
    {
        var updatedCoordinates = new Dictionary<(int, int), AllocationType>();

        foreach (var (key, value) in map.Coordinates)
        {
            switch (value)
            {
                case AllocationType.EnemyShip:
                    updatedCoordinates[key] = AllocationType.AllyShip;
                    break;
                case AllocationType.EnemyHitted:
                    updatedCoordinates[key] = AllocationType.AllyShipHitted;
                    break;
                case AllocationType.AllyShip:
                    updatedCoordinates[key] = AllocationType.EnemyShip;
                    break;
                case AllocationType.AllyShipHitted:
                    updatedCoordinates[key] = AllocationType.EnemyHitted;
                    break;
                case AllocationType.Water:
                case AllocationType.ShotMissed:
                    updatedCoordinates[key] = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        map.Coordinates = updatedCoordinates;
    }
}