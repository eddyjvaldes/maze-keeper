using TheMazeKeeper.Logic.GameElement;
using TheMazeKeeper.Logic.GameCharacter;

namespace TheMazeKeeper.Logic.MapStructure
{
    class Map
    {
        int mapRows;
        MapCell[,] map;
        int density;

        public Map(int mapRows, Hero[] heroes)
        {
            //Init Map
            this.mapRows = mapRows; 

            map = new MapCell[mapRows, mapRows];

            for (int i = 0; i < mapRows; i++)
            {
                for (int j = 0; j < mapRows; j++)
                {
                    map[i, j] = new MapCell();
                }
            }

            for (int i = 0; i < mapRows; i++)
            {
                map[0, i].PlaceElement(new Static("map Wall", 0, i));
                map[i, mapRows - 1].PlaceElement(new Static("map Wall", i, mapRows - 1));
                map[mapRows - 1, i].PlaceElement(new Static("map Wall", mapRows - 1, i));
                map[i, 0].PlaceElement(new Static("map Wall", i, 0));
            }

            //Place heroes
            for (int i = 0; i < heroes.Length; i++)
            {
                int x = (int)heroes[i].Position.X;
                int y = (int)heroes[i].Position.Y;
                
                if (map[x, y].IsAccessible())
                    map[x, y].AddOccupant(heroes[i]);
            }

            //Add Elements
            density = heroes.Length;
            
            //Generate Obstacle
            int count = 0;

            string[] obstacles = {"rock", "tree"};
            Random random = new Random();
            int obstacle;
            MapCell lastObstacle;

            do
            {
                do
                {
                    obstacle = random.Next(obstacles.Length);
                    
                    lastObstacle = addRandomElement(obstacles[obstacle]);
                }
                while(IsMapAccessible());

                lastObstacle.RemoveElement();
                count++;
            }
            while(count < density * mapRows );

            AddRandomGem(1);

            AddRandomTramp(1);
        }

        //Generate Gem
        public void AddRandomGem(int currentTurn)
        {
            int x;
            int y;
            Random random = new Random();

            for (int i = 0; i < mapRows; i++)
            {
                do
                {
                    x = random.Next(mapRows);
                    y = random.Next(mapRows);
                }
                while (!map[x, y].IsValidForPlacement());
                map[x, y].PlaceElement(new Gem(currentTurn * density, currentTurn, x, y));
            }
        }

        //Generate Tramp
        public void AddRandomTramp(int currentTurn)
        {
            string[] tramps = {"Energy Siphon", "Sluggish Field"};
            Random random = new Random();
            int tramp;

            int x;
            int y;

            for (int i = 0; i < mapRows; i++)
            {
                do
                {
                    x = random.Next(mapRows);
                    y = random.Next(mapRows);
                }
                while (!map[x, y].IsValidForPlacement());

                tramp = random.Next(tramps.Length);
                map[x, y].PlaceElement(new Tramp(tramps[tramp],currentTurn, x, y));
            }
        }

        //Element collector
        public void elementCollector(int currentTurn)
        {
            foreach (MapCell cell in map)
            {
                if (cell.HasElement())
                {
                    if (cell.GetElement is Interactive)
                    {
                        Interactive element = (Interactive)cell.GetElement;

                        if (element.Duration > currentTurn)
                            cell.RemoveElement();
                    }
                }
            }
        }

        
        //Generate Static elements
        MapCell addRandomElement(string element)
        {
            Random random = new Random();
            int x;
            int y;

            do
            {
                x = random.Next(mapRows);
                y = random.Next(mapRows);
            }
            while (!map[x, y].IsValidForPlacement());

            map[x, y].PlaceElement(new Static(element, x, y));
            return map[x, y];
        }

        bool IsMapAccessible()
        {
            //local funtion
            void CheckCells(int x, int y, bool[,] checkMask)
            {
                if (checkMask[x, y] == false)
                {
                    checkMask[x, y] = true;

                    if (map[x, y].IsPassable())
                    {
                        CheckCells(x - 1, y, checkMask); //check above
                        CheckCells(x + 1, y, checkMask); //check under
                        CheckCells(x, y - 1, checkMask); //check left
                        CheckCells(x, y + 1, checkMask); //check right
                    }
                }
            }
    
            bool[,] accessibleCells = new bool[mapRows, mapRows];

            //Initial MapCell for check
            Random random = new Random();
            int x;
            int y;

            do
            {
            x = random.Next(1, mapRows - 2);
            y = random.Next(1, mapRows - 2);
            }
            while (!map[x, y].IsPassable());

            //Check map
            CheckCells(x, y, accessibleCells);

            //Check accesibleCells
            bool check = true;

            for (int i = 1; i < mapRows - 1; i++)
            {
                for (int j = 1; j < mapRows - 1; j++)
                {
                    if (!accessibleCells[i, j])
                        check = false;
                }
            }

            return check;
        }

        public MapCell[,] GetCells { get => map; }  
    }
}