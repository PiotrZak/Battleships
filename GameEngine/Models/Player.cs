namespace GameEngine.Models;

public class Player
{
    public Player(string name, bool isAi)
    {
        Name = name;
        IsAi = isAi;
    }

    public string Name { get; set; }
    public int Ranking { get; set; }
    public int Win { get; set; }
    public bool IsAi { get; set; }
}