using GameEngine.Models;

namespace GameEngine.UI
{
    public class GameEngine
    {
        public void StartGame()
        {
            var gameTable = new GameTable();
            gameTable.Start();
        }
    }
}