using TheMazeKeeper.Logic.GameElement;

namespace TheMazeKeeper.Logic.MapStructure
{
    class MapCell
    {
        // Character? occupant;
        Element? gameElement;

        //public bool IsAccessible()
        //{
        //    bool check = false;

        //    if (occupant == null && gameElement.IsPassable)
        //        check = true;
        //    
        //    return check;
        //}

        //Doble check por parte de la clase Hero y MapCell, mejorable.
        //public void AddOccupant(Character character)
        //{
        //    if (occupant == null && gameElement.IsPassable)
        //        occupant = character;
        //}

        public bool IsValidForPlacement()
        {
            bool check = false;

            if (gameElement == null)  //&& occupant == null
                check = true;
            
            return check;
        }

        //Doble check por parte de la clase Map y MapCell, mejorable.
        public void PlaceElement(Element element)
        {
            if (gameElement == null)
                gameElement = element;
        }

        public Element GetElement
        {
            get
            {
                return gameElement;
            }
        }

        public void RemoveElement()
        {
            gameElement = null;
        }

        public bool IsPassable()
        {
            bool check = true;

            if (gameElement != null)
                if (!gameElement.IsPassable)
                    check = false;

            return check;
        }
    }
}