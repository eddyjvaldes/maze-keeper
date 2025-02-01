using Spectre.Console;
using TheMazeKeeper.Logic.GameCharacter;
using TheMazeKeeper.Logic.GameManagement;
using TheMazeKeeper.Logic.MapStructure;

namespace TheMazeKeeper.Console.Game
{
  class GameInit
  {
    Player[] players;
    Hero[] heroes;
    Map map;

    public GameInit()
    {
      int numberOfPlayers = Intro();

      int[] mapDimensions = {15, 17, 21};
      int mapDimension = mapDimensions[numberOfPlayers - 2];

      SelectHeroes(numberOfPlayers, mapDimension);

      if (heroes != null)
      {
        map = new Map(mapDimension, heroes);
      }
    }

    int Intro()
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
              .AddChoices(new[] { "2", "3", "4" }));

      System.Console.Clear();

      return int.Parse(selected);
    }

    void SelectHeroes(int numberOfPlayers, int mapDimension)
    {
      (int, int)[] heroesPositions = { (1, 1), (mapDimension - 2, mapDimension - 2), (1, mapDimension - 2), (mapDimension - 2, 1) };

      players = new Player[numberOfPlayers];
      heroes = new Hero[numberOfPlayers];

      for (int i = 0; i < numberOfPlayers; i++)
      {
        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[blue]Player {i + 1} choose hero[/]")
                .PageSize(5)
                .AddChoices(new[] {
                            "Titania the Swift", "Baron of Brawn", "Blaze the Quick",
                            "Vanguard the Titan", "Phantom the Silent"
                }));

        players[i] = new("Player " + (i + 1));
        heroes[i] = new(players[i], selected, heroesPositions[i].Item1, heroesPositions[i].Item2);

        players[i].SetHero(heroes[i]);

        System.Console.Clear();
      }
    }

    public void OrderPlayersByInitiative()
    {
      Array.Sort(players, (x, y) => y.GetHero.GetInitiative.CompareTo(x.GetHero.GetInitiative));
    }

    public void OrderPlayerByScore()
    {
      Array.Sort(players, (x, y) => y.GetScore.CompareTo(x.GetScore));
    }

    public Player[] GetPlayers
    {
      get { return players; }

      set
      {
        if (value is Player[])
        {
          players = value;
        }
      }
    }

    public Map GetMap { get => map; }
  }
}