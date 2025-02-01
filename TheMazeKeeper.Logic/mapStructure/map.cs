using TheMazeKeeper.Logic.GameElement;
using TheMazeKeeper.Logic.GameCharacter;

namespace TheMazeKeeper.Logic.MapStructure
{
    class Map
    {
        int mapDimension;
        MapCell[,] map;
        int density;

        public Map(int mapDimension, Hero[] heroes)
        {
            this.mapDimension = mapDimension;
            density = heroes.Length;

            map = new MapCell[mapDimension, mapDimension];

            for (int i = 0; i < mapDimension; i++)
            {
                for (int j = 0; j < mapDimension; j++)
                {
                    map[i, j] = new MapCell();
                }
            }

            PlaceMapWalls();

            PlaceHeroes(heroes);

            GenerateObstacles();

            AddGemRandomly();

            AddTrampRandomly();
        }

        public void AddGemRandomly()
        {
            for (int i = 0; i < mapDimension * density/2; i++)
            {
                var coordinates = RandomValidCoordinatesForPlacingElements();

                map[coordinates.Item1, coordinates.Item2].PlaceElement(new Gem(coordinates.Item1, coordinates.Item2));
            }
        }

        public void AddTrampRandomly()
        {
            string[] typeOfTramps = { "Energy Siphon", "Sluggish Field" };

            var random = new Random();

            for (int i = 0; i < mapDimension; i++)
            {
                var coordinates = RandomValidCoordinatesForPlacingElements();

                map[coordinates.Item1, coordinates.Item2].PlaceElement(new Tramp(typeOfTramps[random.Next(typeOfTramps.Length)], coordinates.Item1, coordinates.Item2));
            }
        }

        public void ElementCollector()
        {
            foreach (var cell in map)
            {
                if (cell.GetElement != null)
                {
                    if (cell.GetElement is not Static)
                    {
                        cell.RemoveElement();
                    }
                }
            }
        }

        void GenerateObstacles()
        {
            string[] typeOfObstacles = {"rock", "tree"};

            var random = new Random();

            MapCell CellOfLastObstacle;

            for (int i = 0; i < mapDimension * density; i++)
            {
                do
                {
                    var coordinates = RandomValidCoordinatesForPlacingElements();

                    CellOfLastObstacle = map[coordinates.Item1, coordinates.Item2];

                    CellOfLastObstacle.PlaceElement(new Static(typeOfObstacles[random.Next(typeOfObstacles.Length)], coordinates.Item1, coordinates.Item2));
                }
                while (IsMapAccessible());

                CellOfLastObstacle.RemoveElement();
            }
        }

        void PlaceMapWalls()
        {
            for (int i = 0; i < mapDimension; i++)
            {
                map[0, i].PlaceElement(new Static("map wall", 0, i));
                map[i, mapDimension - 1].PlaceElement(new Static("map wall", i, mapDimension - 1));
                map[mapDimension - 1, i].PlaceElement(new Static("map wall", mapDimension - 1, i));
                map[i, 0].PlaceElement(new Static("map wall", i, 0));
            }
        }

        void PlaceHeroes(Hero[] heroes)
        {
            foreach (var hero in heroes)
            {
                int x = hero.GetPosition().Item1;
                int y = hero.GetPosition().Item2;

                if (map[x, y].IsAccessible())
                {
                    map[x, y].PlaceOccupant(hero);
                }
            }
        }

        public bool MapHasGems()
        {
            bool gem = false;

            foreach (var cell in map)
            {
                if (cell.GetElement is Gem)
                {
                    gem = true;
                    break;
                }
            }

            return gem;
        }

        bool IsMapAccessible()
        {
            var random = new Random();
            int x;
            int y;

            do
            {
                x = random.Next(1, mapDimension - 1);
                y = random.Next(1, mapDimension - 1);
            }
            while (!map[x, y].IsPassable());

            bool[,] passableCells = new bool[mapDimension, mapDimension];

            PassableCells(x, y, passableCells);

            for (int i = 1; i < mapDimension - 1; i++)
            {
                for (int j = 1; j < mapDimension - 1; j++)
                {
                    if (passableCells[i, j] == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        (int, int) RandomValidCoordinatesForPlacingElements()
        {
            var random = new Random();
            int x;
            int y;

            do
            {
                x = random.Next(1, mapDimension - 1);
                y = random.Next(1, mapDimension - 1);
            }
            while (!map[x, y].IsValidForPlacement());

            return (x, y);
        }

        void PassableCells(int x, int y, bool[,] checkMask)
        {
            if (checkMask[x, y] == false)
            {
                checkMask[x, y] = true;

                if (map[x, y].IsPassable())
                {
                    PassableCells(x - 1, y, checkMask); //check above
                    PassableCells(x + 1, y, checkMask); //check under
                    PassableCells(x, y - 1, checkMask); //check left
                    PassableCells(x, y + 1, checkMask); //check right
                }
            }
        }

        public MapCell[,] GetCells { get => map; }

        public int GetDimension { get => mapDimension; }
    }
}