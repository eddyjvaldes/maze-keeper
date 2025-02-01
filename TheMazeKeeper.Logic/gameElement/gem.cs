namespace TheMazeKeeper.Logic.GameElement
{
    class Gem : Element
    {
        int points;

        public Gem(int x, int y)
        {
            this.x = x;
            this.y = y;

            points = 1;

            passable = true;
        }

        public int GetPoints { get => points; }
    }
}