using System.Numerics;

namespace TheMazeKeeper.Logic.GameElement
{
    class Tramp : Interactive
    {
        string effect;

        // name, (effect, duration)
        public Tramp(string name, int currentTurn, int x, int y)
        {
            this.name = name;
            position = new Vector2(x, y);
            passable = true;
            
            Dictionary<string, (string, int)> trampBase = new Dictionary<string, (string, int)>
            {
                {"Energy Siphon", ("Drained",6)},
                {"Sluggish Field", ("Slowed", 4)},
            };

            var attributes = trampBase[name];
            effect = attributes.Item1;
            duration = currentTurn + attributes.Item2;
        }

        public string Effect { get => effect; }
    }
}