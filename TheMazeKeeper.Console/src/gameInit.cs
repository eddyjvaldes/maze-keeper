using System.Numerics;
using Spectre.Console;
using TheMazeKeeper.Logic.GameCharacter;
using TheMazeKeeper.Logic.GameManagement;
using TheMazeKeeper.Logic.MapStructure;

namespace TheMazeKeeper.Console
{
    class GameInit
    {
        Player[] players;
        int mapRow;
        Map map;

        public GameInit()
        {
            Intro();
            CreatePlayers();
            SelectHeroes();
        }

        void Intro()
        {
            System.Console.WriteLine(@"

                                     /@
                     __        __   /\/
                    /==\      /  \_/\/  
                  /======\    \/\__ \__
                /==/\  /\==\    /\_|__ \
             /==/    ||    \=\ / / / /_/
           /=/    /\ || /\   \=\/ /    
        /===/   /   \||/   \   \===\
      /===/   /_________________ \===\
   /====/   / |                /  \====\
 /====/   /   |  _________    /  \   \===\    THE LEGEND OF
 /==/   /     | /   /  \ / / /  __________\_____      ______       ___
|===| /       |/   /____/ / /   \   _____ |\   /      \   _ \      \  \
 \==\             /\   / / /     | |  /= \| | |        | | \ \     / _ \
 \===\__    \    /  \ / / /   /  | | /===/  | |        | |  \ \   / / \ \
   \==\ \    \\ /____/   /_\ //  | |_____/| | |        | |   | | / /___\ \
   \===\ \   \\\\\\\/   /////// /|  _____ | | |        | |   | | |  ___  |
     \==\/     \\\\/ / //////   \| |/==/ \| | |        | |   | | | /   \ |
     \==\     _ \\/ / /////    _ | |==/     | |        | |  / /  | |   | |
       \==\  / \ / / ///      /|\| |_____/| | |_____/| | |_/ /   | |   | |
       \==\ /   / / /________/ |/_________|/_________|/_____/   /___\ /___\
         \==\  /               | /==/
         \=\  /________________|/=/    OCARINA OF TIME
           \==\     _____     /==/
          / \===\   \   /   /===/
         / / /\===\  \_/  /===/
        / / /   \====\ /====/
       / / /      \===|===/
       |/_/         \===/
                      =     
            ");

            var selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]How many players will play?[/]")
                    .PageSize(3)
                    .AddChoices(new[] {"2", "3", "4"}));

            System.Console.Clear();

            int[] mapDimension = {15, 25, 35};
            mapRow = mapDimension[int.Parse(selected) - 2];
            players = new Player[int.Parse(selected)];
        }

        void CreatePlayers()
        {
            string[] playersName = {"Player 1", "Player 2", "Player 3", "Player 4"};

            for (int i = 0; i < players.Length; i++)
                players[i] = new Player(playersName[i]);
        }

        void SelectHeroes()
        {
            Hero[] heroes = new Hero[players.Length];

            Vector2[] heroesPositions = {new Vector2(1, 1), new Vector2(mapRow - 2, mapRow - 2),
                                        new Vector2(1, mapRow - 2), new Vector2(mapRow - 2, 1)};

            for (int i = 0; i < players.Length; i++)
            {
                var selected = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title($"[blue]Player {i + 1} choose hero[/]")
                        .PageSize(5)
                        .AddChoices(new[] {
                            "Titania the Swift", "Baron of Brawn", "Blaze the Quick",
                            "Vanguard the Titan", "Phantom the Silent"
                        }));

                heroes[i] = new Hero(players[i], selected, (int)heroesPositions[i].X, (int)heroesPositions[i].Y);
                players[i].AddHero(heroes[i]);

                System.Console.Clear();
            }

            map = new Map(mapRow, heroes);
        }

        public Player[] GetPlayers { get => players; }

        public Map GetMap { get => map; }

        public int MapRow { get => mapRow; }
    }
}