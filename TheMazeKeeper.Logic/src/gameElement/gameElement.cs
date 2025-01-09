using System.Numerics;

namespace TheMazeKeeper.Logic.GameElement
{
    abstract class Element
    {
        protected string name;
        protected Vector2 position;
        protected bool passable;

        public bool IsPassable { get => passable; }

        public string Getname { get => name; }

        public Vector2 GetPosition { get => position; }
    }
}