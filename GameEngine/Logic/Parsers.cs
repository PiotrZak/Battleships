namespace GameEngine.Logic;

public class Parsers
{
    public static (int, int) ParseUserInput(string? userShotCoordinates)
    {
        string[] parts = userShotCoordinates.Split(',');
        string coordinate = parts[0].Trim();
        char firstChar = char.ToUpper(coordinate[0]);

        if (!int.TryParse(coordinate.Substring(1), out int secondPart))
        {
            throw new ArgumentException("Invalid input format");
        }

        int row = firstChar - 'A' + 1;
        int column = secondPart;

        return(row, column);
        
    }
}