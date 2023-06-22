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
    private readonly Map opponentBoard;
    private readonly Map playerBoard;
    private readonly List<PlayerFleet> fleets;
    private readonly PlayerFleet p1Fleet;
    private readonly PlayerFleet p2Fleet;

    public GameTable()
    {
        map = Map.GenerateMap();
        opponentBoard = Map.GenerateMap();
        playerBoard = Map.GenerateMap();
        var p1 = new Player( "P1", false);
        var p2 = new Player("P2", false);
        p1Fleet = PopulateFleet.BuildPlayerFleet(playerBoard, p1);
        p2Fleet = PopulateFleet.BuildPlayerFleet(opponentBoard, p2);
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

    private static List<PlayerFleet> BuildPlayersFleets(Map opponentBoard, Map playerBoard, Player p1, Player p2)
    {
        var p1Fleet = PopulateFleet.BuildPlayerFleet(playerBoard, p1);
        var p2Fleet = PopulateFleet.BuildPlayerFleet(opponentBoard, p2);
            
        return new List<PlayerFleet> { p1Fleet, p2Fleet };
    }

    private void PlayGame(GameMode mode)
    {
        var currentPlayerIndex = 0;
        var round = 0;
        
        while (!p1Fleet.IsFleetSunk() || !p2Fleet.IsFleetSunk())
        {
            if ((round & 1) != 1)
            {
                playerBoard.DrawMap(false);
                ShotLogic.Shot(p1Fleet, playerBoard, mode, round);
                Console.WriteLine("Player 1 turn.");
            }
            else
            {
                opponentBoard.DrawMap(false);
                ShotLogic.Shot(p2Fleet, opponentBoard, mode, round);
                Console.WriteLine("Player 2 turn.");
            }
            


            
            // this code used when only 1 board
            //ReverseEnemyContext(map);
            //currentPlayerIndex = (currentPlayerIndex + 1) % fleets.Count; // Switch to the next player
            
            round ++;
        }

        Console.WriteLine("Game Over, Rounds: " + round);
        Console.WriteLine("---------");
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