namespace TheMazeKeeper.Logic.GameElement
{
    class Tramp : Element
    {
        string effect;

        public Tramp(string name, int x, int y)
        {
            this.name = name;

            this.x = x;
            this.y = y;

            passable = true;

            // Dictionary<name, effect>
            var trampBase = new Dictionary<string, string>
            {
                {"Energy Siphon", "Drained"},
                {"Sluggish Field", "Slowed"},
            };

            effect = trampBase[name];
        }

        public string GetEffect { get => effect; }
    }
}