namespace GameEngine.Logic;

public class Validators
{
    public static bool IsInputCorrect(string userShotCoordinates)
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
    
    public static bool IsInBoard(int x, int y)
    {
        return x is >= 0 and <= 10 && y is >= 0 and <= 10;
    }
}