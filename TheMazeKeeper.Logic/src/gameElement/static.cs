using System.Numerics;

namespace TheMazeKeeper.Logic.GameElement
{
    class Static : Element
    {
        public Static(string name, int x, int y)
        {
            position = new Vector2(x, y);

            Dictionary<string, bool> baseStatic = new Dictionary<string, bool>
            {
                {"map Wall", false},
                {"tree", false},
                {"rock", false},
            };

            this.name = name;
            passable = baseStatic[name];
        }
    }
}