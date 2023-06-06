using GameEngine.Models;

namespace GameEngine.UI
{
    public class GameEngine
    {

        public int StartGame()
        {
            
            Console.WriteLine("Starting...");

            var p1 = new Player();
            var p2 = new Player();


            var map = GenerateMap();
            
            Console.WriteLine(map.Coordinates);
            
            map.PrintCoordinates();
            map.DrawMap();

            Shot(p1);
            
            return 1;
        }


        public string Shot(Player player)
        {
            
            Console.WriteLine("Where to shot? (eg A5)");
            string userShotCoordinates = Console.ReadLine();

            if(IsInputCorrect(userShotCoordinates))
            {
                Console.WriteLine("Checking for enemy ship...");
            }
            else
            {
                Shot(player);
            }

            return "OK";
        }

        public bool IsInputCorrect(string userShotCoordinates)
        {
            if (userShotCoordinates.Length > 3)
            {
                Console.WriteLine("Coordinates needs to have max 3 characters");
                return false;
            }

            char rowCoordinate = userShotCoordinates[0];
            string columnCoordinate = userShotCoordinates[1..];
            
            if (rowCoordinate is not ('A' or 'B' or 'C' or 'D' or 'E' or 'F' or 'G' or 'H' or 'I' or 'J'))
            {
                Console.WriteLine("First character should be between A and J");
                return false;
            }

            if (columnCoordinate is not ("1" or "2" or "3" or "4" or "5" or "6" or "7" or "8" or "9" or "10"))
            {
                Console.WriteLine("Second character should be between 1 and 10");
                return false;
            }

            return true;
        }

        public static Map GenerateMap()
        {
            var a1 = new Map();
            for (int i = 0; i < 100; i++)
            {
                var point = new[,] { { i, i + 1 } };
                a1.Coordinates.Add(point, AllocationType.Water);
            }

            return a1;
            
        }
    }
}