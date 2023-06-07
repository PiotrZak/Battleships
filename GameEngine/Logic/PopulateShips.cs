using GameEngine.Models;

namespace GameEngine.Logic;

public abstract class PopulateShips
{
    public static PlayerFleet RandomLocalize(Map map, AllocationType allocateType)
    {
        Random random = new Random();
        var fleet = new PlayerFleet();
    
        foreach (ShipClass shipClass in Enum.GetValues(typeof(ShipClass)))
        {
            Console.WriteLine("Placing " + shipClass);
            
            int length = (int)shipClass;
            bool isHorizontal = random.Next(2) == 0; // Randomly determine the ship's orientation
        
            int startX = random.Next(10);
            int startY = random.Next(10);
            
            bool isNotOccupied = false;
            while (!isNotOccupied)
            {
                isNotOccupied = IsPlaceForShip(map, isHorizontal, length, startX, startY);
        
                if (isNotOccupied)
                {
                    var shipPosition = new Dictionary<(int, int), bool>();

                    for (int i = 0; i < length; i++)
                    {
                        var coordinate = isHorizontal ? (startX + i, startY) : (startX, startY + i);
                        shipPosition[coordinate] = true;
                    }

                    var newShip = new Ship(shipPosition, length);
                    fleet.AddShip(shipClass, newShip);
                    PlaceShipOnMap(map, (startX, startY), length, isHorizontal, allocateType);
                }
                else
                {
                    startX = random.Next(10);
                    startY = random.Next(10);
                }
            }
        }

        return fleet;
    }

    public static bool IsPlaceForShip(Map map, bool isHorizontal, int length, int startX, int startY)
    {
        int endX = isHorizontal ? startX + length - 1 : startX;
        int endY = isHorizontal ? startY : startY + length - 1;

        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                if (!map.Coordinates.TryGetValue((x, y), out var value) || value != AllocationType.Water)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static void PlaceShipOnMap(Map map, (int, int) startCoordinates, int length, bool isHorizontal, AllocationType allocateType)
    {
        int startX = startCoordinates.Item1;
        int startY = startCoordinates.Item2;

        for (int i = 0; i < length; i++)
        {
            int coordinateX = isHorizontal ? startX + i : startX;
            int coordinateY = isHorizontal ? startY : startY + i;

            var coordinates = (coordinateX, coordinateY);
            map.Coordinates[coordinates] = allocateType;
        }
    }
}