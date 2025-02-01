using System.Text;
using Spectre.Console;
using TheMazeKeeper.Logic.GameCharacter;
using TheMazeKeeper.Logic.GameElement;
using TheMazeKeeper.Logic.GameManagement;
using TheMazeKeeper.Logic.MapStructure;

namespace TheMazeKeeper.Console.Game
{
    class GameController
    {
        int currentTurn = 1;

        public GameController(GameInit gameInit, int topPoints)
        {
            var players = gameInit.GetPlayers;

            var map = gameInit.GetMap;

            do
            {
                PlayersUpdate(players, currentTurn);

                gameInit.OrderPlayersByInitiative();

                foreach (var player in players)
                {
                    Play(player, map.GetCells, currentTurn);
                }

                currentTurn++;

                if (!map.MapHasGems())
                {
                    map.ElementCollector();
                    map.AddGemRandomly();
                    map.AddTrampRandomly();
                }

            }
            while (!Victory(players, topPoints));

            gameInit.OrderPlayerByScore();

            PrintVictoryMessage(players);
        }

        void Play(Player player, MapCell[,] map, int currentTurn)
        {
            while (player.GetHero.GetEnergy > 0)
            {
                PrintMap(map);
                PrintPlayerInfo(player, currentTurn);

                var selected = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title($"[blue]{player.GetName} choose action[/]")
                        .PageSize(3)
                        .AddChoices(new[] { "Move", "Use Power", "Help" }));

                if (selected == "Move")
                {
                    var direction = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title($"[blue]{player.GetName} choose direction[/]")
                            .PageSize(4)
                            .AddChoices(new[] { "up", "down", "right", "left" }));

                    if (direction == "up")
                    {
                        if (!player.Move(1, map, currentTurn))
                        {
                            System.Console.WriteLine("Invalid direction");
                            System.Console.ReadLine();
                        }
                    }
                    else if (direction == "down")
                    {
                        if (!player.Move(2, map, currentTurn))
                        {
                            System.Console.WriteLine("Invalid direction");
                            System.Console.ReadLine();
                        }
                    }
                    else if (direction == "right")
                    {
                        if (!player.Move(3, map, currentTurn))
                        {
                            System.Console.WriteLine("Invalid direction");
                            System.Console.ReadLine();
                        }
                    }
                    else if (direction == "left")
                    {
                        if (!player.Move(4, map, currentTurn))
                        {
                            System.Console.WriteLine("Invalid direction");
                            System.Console.ReadLine();
                        }
                    }
                }
                else if (selected == "Use Power")
                {
                    if (player.UsePower(currentTurn))
                    {
                        System.Console.WriteLine($"Use {player.GetHero.GetPower.GetName}");
                    }
                    else
                    {
                        System.Console.WriteLine("It is not possible to use the power");
                    }

                    System.Console.ReadLine();
                }
                else if (selected == "Help")
                {
                    AnsiConsole.Markup("\n- :black_large_square: are the walls of the map.\n- :deciduous_tree: and :rock: are obstacles.\n- :gem_stone: are items that provide points.\n- :bomb: are items that negatively affect heroes.");
                    System.Console.ReadLine();
                }

                System.Console.Clear();
            }

            System.Console.WriteLine("Your hero is exhausted");
            System.Console.ReadLine();

            System.Console.Clear();
        }

        void PrintMap(MapCell[,] map)
        {
            int mapDimension = map.GetLength(0);

            for (int i = 0; i < mapDimension; i++)
            {
                for (int j = 0; j < mapDimension; j++)
                {
                    if (map[i, j].GetElement != null || map[i, j].GetOccupant != null)
                    {
                        var occupant = map[i, j].GetOccupant;
                        var element = map[i, j].GetElement;

                        if (occupant != null)
                        {
                            PrintOccupant(occupant);
                        }
                        else if (element != null)
                        {
                            PrintElement(element);
                        }
                    }
                    else
                    {
                        AnsiConsole.Markup(":green_square:");
                    }
                }

                System.Console.WriteLine();
            }
        }

        void PrintOccupant(Hero hero)
        {
            string playerName = hero.GetPlayer.GetName;

            if (playerName == "Player 1")
            {
                AnsiConsole.Markup(":bison:");
            }
            else if (playerName == "Player 2")
            {
                AnsiConsole.Markup(":boar:");
            }
            else if (playerName == "Player 3")
            {
                AnsiConsole.Markup(":goat:");
            }
            else if (playerName == "Player 4")
            {
                AnsiConsole.Markup(":elephant:");
            }
        }

        void PrintElement(Element element)
        {
            if (element is Static)
            {
                if (element.GetName == "map wall")
                {
                    AnsiConsole.Markup(":black_large_square:");
                }
                else if (element.GetName == "tree")
                {
                    AnsiConsole.Markup(":deciduous_tree:");
                }
                else if (element.GetName == "rock")
                {
                    AnsiConsole.Markup(":rock:");
                }
            }
            else if (element is Gem)
            {
                AnsiConsole.Markup(":gem_stone:");
            }
            else if (element is Tramp)
            {
                AnsiConsole.Markup(":bomb:");
            }
        }

        void PlayersUpdate(Player[] players, int currentTurn)
        {
            foreach (var player in players)
            {
                player.GetHero.HeroRecovery();
                player.GetHero.UpdateStatus(currentTurn);
            }
        }

        void PrintPlayerInfo(Player player, int currentTurn)
        {
            AnsiConsole.Markup($"\nturn {currentTurn}\n{PlayerIcon(player)} {player.GetName} ({player.GetHero.GetName}):\n- score: {player.GetScore}\n- energy: {player.GetHero.GetEnergy}\n- hero states: {HeroStates(player.GetHero.GetActiveStatesList, currentTurn)}\n");
        }

        string PlayerIcon(Player player)
        {
            string playerName = player.GetName;
            string icon;

            if (playerName == "Player 1")
            {
                icon = ":bison:";
            }
            else if (playerName == "Player 2")
            {
                icon = ":boar:";
            }
            else if (playerName == "Player 3")
            {
                icon = ":goat:";
            }
            else
            {
                icon = ":elephant:";
            }

            return icon;
        }

        string HeroStates(List<Logic.GameCharacter.Status> StatesList, int currentTurn)
        {
            StringBuilder statesMessage = new StringBuilder();

            foreach (var status in StatesList)
            {
                statesMessage.Append($"{status.GetName} (energy: {status.GetEffect().Item1}, initiative: {status.GetEffect().Item2}) duration: {status.GetDuration - currentTurn}\n");
            }

            return statesMessage.ToString();
        }

        bool Victory(Player[] players, int topPoints)
        {
            foreach (var player in players)
            {
                if (player.GetScore >= topPoints)
                {
                    return true;
                }
            }

            return false;
        }

        void PrintVictoryMessage(Player[] players)
        {
            System.Console.WriteLine($"Congratulations {players[0].GetName}, you won the game");

            System.Console.WriteLine("Ranking list:");

            foreach (var player in players)
            {
                System.Console.WriteLine($"{player.GetName} Score: {player.GetScore}");
            }
        }
    }
}