using System.Numerics;

namespace TheMazeKeeper.Logic.GameElement
{
    class Gem : Interactive
    {
        int points;

        public Gem(int points, int currentTurn, int x, int y)
        {
            this.points = points;
            position = new Vector2(x, y);
            passable = true;
            duration = currentTurn;
        }

        public void Grow(int extraPoints)
        {
            points+= extraPoints;
        }    

        public int GetPoints { get => points; }
    }   
}