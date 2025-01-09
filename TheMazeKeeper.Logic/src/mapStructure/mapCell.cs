using TheMazeKeeper.Logic.GameCharacter;
using TheMazeKeeper.Logic.GameElement;

namespace TheMazeKeeper.Logic.MapStructure
{
    class MapCell
    {
        Hero? occupant;
        Element? gameElement;

        public void PlaceElement(Element element)
        {
            if (gameElement == null)
                gameElement = element;
        }

        public bool IsPassable()
        {
            bool check = true;

            if (gameElement != null)
                if (!gameElement.IsPassable)
                    check = false;

            return check;
        }

        public bool IsValidForPlacement()
        {
            bool check = false;

            if (gameElement == null && occupant == null)
                check = true;
            
            return check;
        }

        public void AddOccupant(Hero hero)
        {
            if (occupant == null)
            {
                if (gameElement == null)
                    occupant = hero;
                else if (gameElement.IsPassable)
                    occupant = hero;
            }
        }

        public bool IsAccessible()
        {
            bool check = false;

            if (occupant == null)
            {
                if (gameElement == null)
                    check = true;
                else if (gameElement.IsPassable)
                    check = true;
            }

            return check;
        }

        public void RemoveElement() { gameElement = null; }

        public bool HasElement()
        {
            return gameElement != null;
        }
        public Element? GetElement { get => gameElement; }

        public Hero? GetOccupant { get => occupant; }
    }
}