using System.Text;
using Spectre.Console;
using TheMazeKeeper.Logic.GameElement;
using TheMazeKeeper.Logic.GameManagement;
using TheMazeKeeper.Logic.MapStructure;

namespace TheMazeKeeper.Console
{
    class GameController
    {
        int currentTurn = 1;

        public GameController(Player[] players, Map map, int mapRow, int TopPoints)
        {
            do
            {
                PlayersUpdate(players, currentTurn);

                SortedBySpeed(players);

                for (int i = 0; i < players.Length; i++)
                {
                    Play(players[i], map, mapRow, currentTurn);
                }

                currentTurn++;

                if ( currentTurn % 5 == 0)
                {
                    map.elementCollector(currentTurn);
                    map.AddRandomGem(currentTurn);
                    map.AddRandomTramp(currentTurn);
                }

            } while (!Victory(players));

            PrintVictoryMessage(players);
        }

        void SortedBySpeed(Player[] players)
        {
            void Swap(Player[] players, int x, int y)
            {
                Player copy = players[y];

                players[y] = players[x];
                players[x] = copy;
            }

            for (int i = 0; i < players.Length; i++)
            {
                for (int j = 0; j < players.Length - 1; j++) // players.Length--
                {
                    if (players[j].GetHero.GetIniciative < players[j + 1].GetHero.GetIniciative)
                        Swap(players, j, j + 1);
                }
            }
        }

        void PrintMap(Map map, int mapRow)
        {
            for (int i = 0; i < mapRow; i++)
            {
                for (int j = 0; j < mapRow; j++)
                {
                    if (map.GetCells[i, j].HasElement()|| map.GetCells[i, j].HasOccupant())
                    {
                        if (map.GetCells[i, j].HasOccupant())
                        {
                            string playerName = map.GetCells[i, j].GetOccupant.GetPlayer.Name;
                            if (playerName == "Player 1")
                                AnsiConsole.Markup(":bison: ");
                            else if (playerName == "Player 2")
                                AnsiConsole.Markup(":boar:");
                            else if (playerName == "Player 3")
                                AnsiConsole.Markup(":goat:");
                            else
                                AnsiConsole.Markup(":elephant:");
                        }
                        else
                        {
                            Element element = map.GetCells[i, j].GetElement;

                            if (element is Static)
                            {
                                if (element.Getname == "map Wall")
                                    AnsiConsole.Markup(":black_large_square:");
                                else if (element.Getname == "tree")
                                    AnsiConsole.Markup(":deciduous_tree:");
                                else if (element.Getname == "rock")
                                    AnsiConsole.Markup(":rock: ");
                            }
                            else if (element is Gem)
                                AnsiConsole.Markup(":gem_stone:");
                            else if (element is Tramp)
                                AnsiConsole.Markup(":bomb:");
                        }

                    }
                    else
                        AnsiConsole.Markup(":green_square:");
                }

                System.Console.WriteLine();
            }
        }

        void Play(Player player, Map map, int mapRow, int currentTurn)
        {
            string HeroStates(Player player, int currentTurn)
            {
                List<Logic.GameCharacter.Status> heroStates = player.GetHero.GetListStates;
                StringBuilder states = new StringBuilder();

                for (int i = 0; i < heroStates.Count; i++)
                {
                    string statusName = heroStates[i].Name;
                    int duration = heroStates[i].Duration - currentTurn;

                    states.Append($"{statusName} duration: {duration}, ");
                }

                return states.ToString();
            }

            string PlayerIcon()
            {
                string playerName = player.Name;
                string icon;

                if (playerName == "Player 1")
                    icon = ":bison: ";
                else if (playerName == "Player 2")
                    icon =":boar:";
                else if (playerName == "Player 3")
                    icon = ":goat:";
                else
                    icon = ":elephant:";

                return icon;
            }
            
            void PrintPlayerInfo()
            {
                AnsiConsole.Markup(@$"{PlayerIcon()} {player.Name}:
                - score: {player.GetScore}
                - energy: {player.GetHero.GetEnergy}
                - hero states: {HeroStates(player, currentTurn)}
                                        ");
            }

            while (player.GetHero.GetEnergy > 0)
            {
                PrintMap(map, mapRow);
                System.Console.WriteLine();
                PrintPlayerInfo();
                System.Console.WriteLine();

                var selected = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title($"[blue]{player.Name} choose action[/]")
                        .PageSize(3)
                        .AddChoices(new[] {"Move", "Use Power", "Help"}));

                if (selected == "Move")
                {

                    var directon = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title($"[blue]{player.Name} choose direction[/]")
                            .PageSize(4)
                            .AddChoices(new[] {"up", "down", "right", "left"}));
                    
                    if (directon == "up")
                    {
                        if(!player.Move(1, map.GetCells, currentTurn))
                        {
                            System.Console.WriteLine("Invalid direction");
                            System.Console.ReadLine();
                        }
                    }

                    else if (directon == "down")
                    {
                        if(!player.Move(2, map.GetCells, currentTurn))
                        {
                            System.Console.WriteLine("Invalid direction");
                            System.Console.ReadLine();
                        }
                    }

                    else if (directon == "right")
                    {
                        if(!player.Move(3, map.GetCells, currentTurn))
                        {
                            System.Console.WriteLine("Invalid direction");
                            System.Console.ReadLine();
                        }
                    }

                    else
                    {
                        if(!player.Move(4, map.GetCells, currentTurn))
                        {
                            System.Console.WriteLine("Invalid direction");
                            System.Console.ReadLine();
                        }
                    }
                }
                else if (selected == "Use Power")
                {
                    if (player.UsePower(currentTurn))
                        System.Console.WriteLine($"Use {player.GetHero.GetPower.Name}");
                    else
                        System.Console.WriteLine("It is not possible to use the power");

                    System.Console.ReadLine();
                }
                else
                {
                    AnsiConsole.Markup(@"
                    - :black_large_square: are the walls of the map.
                    - :deciduous_tree: and :rock: are obstacles.
                    - :gem_stone: are items that provide points.
                    - :bomb: are items that negatively affect heroes.
                    ");

                    System.Console.ReadLine();
                }    

                System.Console.Clear();
            }

            System.Console.WriteLine("Your hero is exhausted");
            System.Console.ReadLine();
        }

        void PlayersUpdate(Player[] players, int currentTurn)
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].GetHero.HeroRecovery();
                players[i].GetHero.UpdateStatusEffects(currentTurn);
            }
        }

        bool Victory(Player[] players)
        {
            bool check = false;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetScore >= 30)
                {
                    check = true;
                    break;
                }
            }

            return check;
        }

        void PrintVictoryMessage(Player[] players)
        {
            void Swap(Player[] players, int x, int y)
            {
                Player copy = players[y];

                players[y] = players[x];
                players[x] = copy;
            }

            for (int i = 0; i < players.Length; i++)
            {
                for (int j = 0; j < players.Length - 1; j++) // players.Length--
                {
                    if (players[j].GetScore < players[j + 1].GetScore)
                        Swap(players, j, j + 1);
                }
            }

            System.Console.WriteLine($"Congratulations {players[0].Name}, you won the game");
            System.Console.WriteLine("Ranking list:");

            for (int i = 0; i < players.Length; i++)
            {
                System.Console.WriteLine($"{players[0].Name} Score: {players[0].GetScore}");
            }
        }
    }   
}