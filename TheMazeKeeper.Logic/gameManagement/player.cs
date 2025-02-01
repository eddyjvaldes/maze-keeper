using TheMazeKeeper.Logic.GameCharacter;
using TheMazeKeeper.Logic.MapStructure;

namespace TheMazeKeeper.Logic.GameManagement
{
    class Player
    {
        string name;
        Hero? hero;
        int score = 0;

        public Player(string name)
        {
            this.name = name;
        }

        public bool Move(int direction, MapCell[,] map, int currentTurn)
        {
            bool movement = false;

            if (hero != null)
            {
                switch (direction)
                {
                    case 1:     // check above
                        movement = hero.Move(-1, 0, map, currentTurn);
                        break;
                    case 2:     // check under
                        movement = hero.Move(1, 0, map, currentTurn);
                        break;
                    case 3:     // check right
                        movement = hero.Move(0, 1, map, currentTurn);
                        break;
                    case 4:     // check left
                        movement = hero.Move(0, -1, map, currentTurn);
                        break;
                }
            }

            return movement;
        }

        public bool UsePower(int currentTurn)
        {
            if (hero != null)
            {
                return hero.UsePower(currentTurn);
            }

            return false;
        }

        public void AddScore(int points)
        {
            if (points > 0)
            {
                score+= points;
            }
        }

        public string GetName { get => name; }

        public Hero? GetHero { get => hero; }

        public void SetHero(Hero hero)
        {
            if (hero.GetPlayer == this)
            {
                this.hero = hero;
            }
        }

        public int GetScore { get => score; }
    }
}