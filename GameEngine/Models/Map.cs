namespace GameEngine.Models;

public enum AllocationType
{
    Water = '~',
    EnemyHitted = '*',
    EnemyShip = 'F',
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
    
    public void DrawMap()
    {
        int rowCount = 10;
        int columnCount = 10;
        int index = 0;

        // Print column headers (1-10)
        Console.Write("  ");
        for (int j = 0; j <= columnCount; j++)
        {
            Console.Write(j);
        }
        Console.WriteLine();

        // Print rows with headers (A-J) and grid elements
        for (int i = 0; i < rowCount; i++)
        {
            Console.Write((char)('A' + i)); // Print row header (A-J)

            for (int j = 0; j < columnCount; j++)
            {
                AllocationType positionAllocation = Coordinates.Values.ElementAt(index);
                Console.Write((char)positionAllocation);
                index++;
            }
            Console.WriteLine();
        }
        
        Console.WriteLine("Number of ally fields" + GetNumberOfAllyLifes());
        Console.WriteLine("Number of enemy fields:" + GetNumberOfEnemyLifes());
    }
}
    