using GameEngine.Logic;
using GameEngine.Models;

namespace GameEngine.UI
{
    public class GameEngine
    {
        public void StartGame()
        {
            
            Console.WriteLine("Starting...");
            var map = GenerateMap();
            
            Console.WriteLine("Welcome to the Game!");
            Console.WriteLine("Please select a game mode:");
            Console.WriteLine("1. 2 Players");
            Console.WriteLine("2. 1 Player vs AI");
            
            string input = Console.ReadLine();

            Player? p1;
            Player? p2;
            
            switch (input)
            {
                case "1":
                    Console.WriteLine("You selected 2 Players mode.");
                    p1 = new Player( "P1", false);
                    p2 = new Player("P2", false);
                    Console.WriteLine("Configuring...");
                    ConfigureGame(map, p1, p2 );
                    break;
                case "2":
                    Console.WriteLine("You selected 1 Player vs AI mode.");
                    Console.WriteLine("Sorry... This path is not implemented yet");
                    p1 = new Player( "P1", false);
                    p2 = new Player("P2", true);
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
            
            Console.WriteLine(map.Coordinates);
            
            map.DrawMap();
            // Shot(p1, map);
            // map.DrawMap();
        }

        public void ConfigureGame(Map map, Player p1, Player p2)
        {
            var p1Fleet = PopulateFleet.BuildPlayerFleet(map, p1);
            var p2Fleet = PopulateFleet.BuildPlayerFleet(map, p2);
        }
        
        public string Shot(Player player, Map map)
        {
            
            Console.WriteLine("Where to shot? (eg A5)");
            string userShotCoordinates = Console.ReadLine();

            if(Validators.IsInputCorrect(userShotCoordinates))
            {
                var (x, y) = Parsers.ParseUserInput(userShotCoordinates);
                
                Console.WriteLine("Checking for enemy ship in " + x + y);
                
                if (map.Coordinates.TryGetValue((x, y), out var allocationType))
                {
                    Console.WriteLine("Value found: " + allocationType);
                    
                    
                    // Checks for Ships
                    
                    map.Coordinates[(x, y)] = AllocationType.ShotMissed;
                }
                else
                {
                    Console.WriteLine("Key not found");
                }
            }
            else
            {
                Shot(player, map);
            }

            return "OK";
        }

        public void HitShip((int, int) coordinates)
        {
            
        }
        
        public static Map GenerateMap()
        {
            var map = new Map();
            for (int x = 1; x <= 10; x++)
            {
                for (int y = 1; y <= 10; y++)
                {
                    var point = (x, y);
                    map.Coordinates.Add(point, AllocationType.Water);
                }
            }

            return map;
        }
    }
}