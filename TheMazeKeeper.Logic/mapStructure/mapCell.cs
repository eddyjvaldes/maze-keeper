using TheMazeKeeper.Logic.GameCharacter;
using TheMazeKeeper.Logic.GameElement;

namespace TheMazeKeeper.Logic.MapStructure
{
    class MapCell
    {
        Hero? occupant;
        Element? element;

        public bool PlaceOccupant(Hero hero)
        {
            if (occupant == null)
            {
                if (IsAccessible())
                {
                    occupant = hero;

                    return true;
                }
            }

            return false;
        }

        public bool PlaceElement(Element element)
        {
            bool placedElement = false;

            if (this.element == null)
            {
                if (occupant == null)
                {
                    this.element = element;

                    placedElement = true;
                }
                else if (element.IsPassable)
                {
                    this.element = element;

                    placedElement = true;
                }
            }

            return placedElement;
        }

        public bool IsAccessible()
        {
            bool accessible = false;

            if (occupant == null)
            {
                if (element == null)
                {
                    accessible = true;
                }
                else if (element.IsPassable)
                {
                    accessible = true;
                }
            }

            return accessible;
        }

        public bool IsPassable()
        {
            bool passable = true;

            if (element != null)
            {
                if (!element.IsPassable)
                {
                    passable = false;
                }
            }

            return passable;
        }

        public bool IsValidForPlacement()
        {
            if (element == null && occupant == null)
            {
                return true;
            }

            return false;
        }

        public void RemoveElement()
        {
            element = null;
        }

        public void RemoveOccupant()
        {
            occupant = null;
        }

        public Element? GetElement { get => element; }

        public Hero? GetOccupant { get => occupant; }
    }
}