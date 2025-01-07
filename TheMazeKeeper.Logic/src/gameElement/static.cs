using System.Numerics;

namespace TheMazeKeeper.Logic.GameElement
{
    class Static : Element
    {
        internal Static(string name, int x, int y)
        {
            position = new Vector2(x, y);

            Dictionary<string, bool> baseStatic = new Dictionary<string, bool>
            {
                {"mapWall", false},
                {"tree", false},
                {"rock", false},
            };

            this.name = name;
            passable = baseStatic[name];
        }
    }
}