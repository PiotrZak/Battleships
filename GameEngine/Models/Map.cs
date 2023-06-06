namespace GameEngine.Models;

public enum AllocationType
{
    Water = '~',
    EnemyHitted = 'E',
    ShotMissed = 'M',
    AllyShip = 'A'
}

public class Map
{
    public Map()
    {
        Coordinates = new Dictionary<int[,], AllocationType>();
    }

    public Dictionary<int[,], AllocationType> Coordinates { get; set; }

    public void PrintCoordinates()
    {
        Console.WriteLine("Map generating...");
        foreach (var coordinate in Coordinates)
        {
            int x = coordinate.Key[0, 0];
            int y = coordinate.Key[0, 1];

            Console.WriteLine($"Point: ({x}, {y})");
        }
    }

    public void DrawMap()
    {
        Console.WriteLine(Coordinates);
        Console.WriteLine(Coordinates.Keys);
        
        foreach(var positionAllocation in Coordinates.Values)
        {
            Console.WriteLine(positionAllocation);
        }
        
    }
}
    