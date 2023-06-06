using GameEngine.Logic;
using GameEngine.Models;

namespace GameEngine.UI
{
    public class GameEngine
    {
        public int StartGame()
        {
            
            Console.WriteLine("Starting...");
            var map = GenerateMap();
            
            Console.WriteLine("Configuring...");
            ConfigureGame(map);
            
            var p1 = new Player();
            var p2 = new Player();
            
            Console.WriteLine(map.Coordinates);
            
            map.DrawMap();
            Shot(p1, map);
            map.DrawMap();
            
            return 1;
        }

        public void ConfigureGame(Map map)
        {
            PopulateShips.BuildShips(map);
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