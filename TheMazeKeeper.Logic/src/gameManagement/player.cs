using System.Numerics;
using TheMazeKeeper.Logic.GameCharacter;
using TheMazeKeeper.Logic.MapStructure;

namespace TheMazeKeeper.Logic.GameManagement
{
    class Player
    {
        string name;
        int score = 0;
        Hero? hero;

        public Player(string name)
        {
            this.name = name;
        }

        public bool Move(int direction, MapCell[,] map, int currentTurn)
        {
            int check = hero.GetEnergy;

            switch (direction)
            {
                //check above
                case 1:
                    hero.MoveDirection(new Vector2(-1, 0), map, currentTurn);
                    break;
                //check under
                case 2:
                    hero.MoveDirection(new Vector2(1, 0), map, currentTurn);
                    break;
                //check right
                case 3:
                    hero.MoveDirection(new Vector2(0, 1), map, currentTurn);
                    break;
                //check left
                case 4:
                    hero.MoveDirection(new Vector2(0, -1), map, currentTurn);
                    break;
            }

            return check != hero.GetEnergy;
        }
    
        public bool UsePower(int currentTurn)
        {
            int check = hero.GetPower.Cooldown;

            hero.UsePower(currentTurn);

            return check != hero.GetPower.Cooldown;
        }

        public void AddScore(int points)
        {
            score+= points;
        }

        public void AddHero(Hero hero)
        {
            this.hero = hero;
        }

        public string Name { get => name; }

        public Hero GetHero { get => hero; }

        public int GetScore { get => score; }
    }
}