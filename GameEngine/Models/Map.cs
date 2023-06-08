namespace GameEngine.Models;

public enum AllocationType
{
    Water = '~',
    EnemyHitted = '*',
    EnemyShip = 'E',
    ShotMissed = 'M',
    AllyShip = 'A',
    AllyShipHitted = '#',
}

public class Map
{
    public Map()
    {
        Coordinates = new Dictionary<(int, int), AllocationType>();
    }

    public Dictionary<(int, int), AllocationType> Coordinates { get; set; }

    private int GetNumberOfAllyLifes()
    {
        //17
        return Coordinates.Count(x => x.Value == AllocationType.AllyShip);
    }

    private int GetNumberOfEnemyLifes()
    {
        //17
        return Coordinates.Count(x => x.Value == AllocationType.EnemyShip);
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
    
    public void DrawMap(bool isMasked)
    {
        int rowCount = 10;
        int columnCount = 10;
        int index = 0;

        // Print column headers (1-10)
        Console.Write("   ");
        for (int j = 1; j <= columnCount; j++)
        {
            Console.Write("[");
            Console.Write(j);
            Console.Write("]");
        }
        Console.WriteLine();

        // Print rows with headers (A-J) and grid elements
        for (var i = 0; i < rowCount; i++)
        {
            Console.Write("[");
            Console.Write((char)('A' + i)); // Print row header (A-J)
            Console.Write("]");
            
            for (var j = 0; j < columnCount; j++)
            {
                var positionAllocation = Coordinates.Values.ElementAt(index);
                if (isMasked)
                {
                    if (positionAllocation == AllocationType.EnemyShip)
                    {
                        Console.Write((char)AllocationType.Water);
                    }
                    else
                    {
                        ColorAllocationType(positionAllocation);
                    }
                }
                else
                {
                    ColorAllocationType(positionAllocation);
                }
                index++;
            }
            Console.WriteLine();
        }
        
        Console.WriteLine("Number of ally fields" + GetNumberOfAllyLifes());
        Console.WriteLine("Number of enemy fields:" + GetNumberOfEnemyLifes());
    }

    private static void ColorAllocationType(AllocationType allocationType)
    {
        switch (allocationType)
        {
            case AllocationType.ShotMissed:
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("[");
                Console.Write((char)allocationType);
                Console.Write("]");
                Console.ResetColor();
                break;
            case AllocationType.Water:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("[");
                Console.Write((char)allocationType);
                Console.Write("]");
                Console.ResetColor();
                break;
            case AllocationType.EnemyHitted or AllocationType.EnemyShip:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[");
                Console.Write((char)allocationType);
                Console.Write("]");
                Console.ResetColor();
                break;
            case AllocationType.AllyShip or AllocationType.AllyShipHitted:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("[");
                Console.Write((char)allocationType);
                Console.Write("]");
                Console.ResetColor();
                break;  
        }
    }
    
    
}
    