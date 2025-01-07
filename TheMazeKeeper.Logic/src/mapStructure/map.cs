using TheMazeKeeper.Logic.GameElement;

namespace TheMazeKeeper.Logic.MapStructure
{
    class Map
    {
        int mapRows;
        MapCell[,] map;
        int density;

        public Map(int mapRows, int characters) //Character[] characters
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
                map[0, i].PlaceElement(new Static("mapWall", 0, i));
                map[i, mapRows - 1].PlaceElement(new Static("mapWall", i, mapRows - 1));
                map[mapRows - 1, i].PlaceElement(new Static("mapWall", mapRows - 1, i));
                map[i, 0].PlaceElement(new Static("mapWall", i, 0));
            }

            /*Place character
            for (int i = 0; i < characters.Length; i++)
                map[characters[i].GetPosition.x, characters[i].GetPosition.y].AddOccupant(characters[i]);
            */
            //Add Elements
            density = characters; //characters.Length;
            
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
            while(count < density * mapRows/2);            

            //Generate Gem


            //Generate Tramp
        }

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
            x = random.Next(mapRows);
            y = random.Next(mapRows);
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

        public MapCell[,] GetCell
        {
            get
            {
                return map;
            }
        }        
    }
}