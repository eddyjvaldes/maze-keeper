namespace TheMazeKeeper.Logic.GameElement
{
    class Static : Element
    {
        public Static(string name, int x, int y)
        {
            this.name = name;

            this.x = x;
            this.y = y;

            // Dictionary <name, passable>
            var staticBase = new Dictionary<string, bool>
            {
                {"map wall", false},
                {"tree", false},
                {"rock", false},
            };

            passable = staticBase[name];
        }
    }
}