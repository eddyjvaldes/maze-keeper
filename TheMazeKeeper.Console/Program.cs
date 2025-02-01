namespace TheMazeKeeper.Console.Game
{
    class Program
    {
        static void Main()
        {
            var gameInit = new GameInit();

            new GameController(gameInit, 10);
        }
    }
}