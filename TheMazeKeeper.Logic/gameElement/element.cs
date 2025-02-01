namespace TheMazeKeeper.Logic.GameElement
{
    abstract class Element
    {
        protected string name = "";
        protected bool passable;

        protected int x;
        protected int y;

        public (int, int) GetPosition()
        {
            return (x, y);
        }

        public bool IsPassable { get => passable; }

        public string GetName { get => name; }
    }
}