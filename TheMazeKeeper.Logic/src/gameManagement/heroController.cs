using System.Numerics;
using TheMazeKeeper.Logic.GameCharacter;
using TheMazeKeeper.Logic.MapStructure;

namespace TheMazeKeeper.Logic.GameManagement
{
    class HeroController
    {
        string name;
        int score = 0;
        Hero hero;

        HeroController(string name, Hero hero)
        {
            this.name = name;
            this.hero = hero;
        }

        public bool Move(int direction, MapCell[,] map)
        {
            int check = hero.Energy;

            switch (direction)
            {
                //check above
                case 1:
                    hero.MoveDirection(new Vector2(-1, 0), map);
                    break;
                //check under
                case 2:
                    hero.MoveDirection(new Vector2(1, 0), map);
                    break;
                //check right
                case 3:
                    hero.MoveDirection(new Vector2(0, 1), map);
                    break;
                //check left
                case 4:
                    hero.MoveDirection(new Vector2(0, -1), map);
                    break;
            }

            return check != hero.Energy;
        }
    
        public bool UsePower(int currentTurn)
        {
            int check = hero.Power.Cooldown;

            hero.UsePower(currentTurn);

            return check != hero.Power.Cooldown;
        } 
    }
}