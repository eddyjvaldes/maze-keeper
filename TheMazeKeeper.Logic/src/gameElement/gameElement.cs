using System.Numerics;

namespace TheMazeKeeper.Logic.GameElement
{
    abstract class Element
    {
        protected string name;
        protected Vector2 position;
        protected bool passable;

        public string Getname
        {
            get
            {
                return name;
            }
        }

        public Vector2 GetPosition
        {
            get
            {
                return position;
            }
        }

        public bool IsPassable
        {
            get
            {
                return passable;
            }
        }
    }
}