using System.Numerics;
using TheMazeKeeper.Logic.GameCharacter;
using TheMazeKeeper.Logic.GameElement;
using TheMazeKeeper.Logic.MapStructure;

namespace TheMazeKeeper.MapTests
{
    public class MapTests
    {
        [Theory]
        [InlineData(75)]
        [InlineData(105)]
        [InlineData(125)]
        public void TestGenerateMap_CreatesValidMap(int mapDimension)
        {
            Map testMap = GenerateTestMap(mapDimension, 1);

            Assert.True(VerifyMapBordersAreWalls(testMap));
            Assert.True(VerifyMapHasAtLeastOneObstacle(testMap));
            Assert.True(VerifyMapIsAccessible(testMap));
        }

        [Theory]
        [InlineData(75, 2)]
        [InlineData(105, 3)]
        [InlineData(125, 4)]
        public void TestHeroPlacementOnMap(int mapDimension, int numberOfHeroes)
        {
            Map testMap = GenerateTestMap(mapDimension, numberOfHeroes);

            Assert.True(VerifyHeroPlacement(testMap));
            Assert.True(VerifyHeroOnBlockedCell(testMap));
        }

        private Map GenerateTestMap(int dimension, int numberOfHeroes)
        {
            Vector2[] heroesPositions = {new Vector2(1, 1), new Vector2(dimension - 2, dimension - 2),
                                        new Vector2(1, dimension - 2), new Vector2(dimension - 2, 1)};

            Hero[] heroes = new Hero[numberOfHeroes];

            for (int i = 0; i < numberOfHeroes; i++)
            {
                heroes[i] = new Hero("Titania the Swift", (int)heroesPositions[i].X, (int)heroesPositions[i].Y);
            }

            return new Map(dimension, heroes);
        }

        private bool VerifyMapBordersAreWalls(Map map)
        {
            bool check = true;

            bool IsWall(int x, int y)
            {
                bool check = true;

                if (map.GetCell[x, y].HasElement())
                {
                    if (map.GetCell[x, y].GetElement.Getname != "map Wall")
                        check = false;
                }
                else check = false;

                return check;
            }

            for (int i = 0; i < map.GetCell.GetLength(0); i++)
            {
                if (!(IsWall(0, i) &&
                      IsWall(i, map.GetCell.GetLength(0) - 1) &&
                      IsWall(map.GetCell.GetLength(0) - 1, i) &&
                      IsWall(i, 0)))
                { 
                    check = false;
                    break;
                }
            }

            return check;
        }

        private bool VerifyMapHasAtLeastOneObstacle(Map map)
        {
            bool check = false;

            for (int i = 1; i < map.GetCell.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < map.GetCell.GetLength(0) - 1; j++)
                {
                    if (map.GetCell[i, j].GetElement is Static)
                    {
                        check = true;
                        break;
                    }
                }
            }

            return check;
        }

        private bool VerifyMapIsAccessible(Map map)
        {
            //local funtion
            void CheckCells(int x, int y, bool[,] checkMask)
            {
                if (checkMask[x, y] == false)
                {
                    checkMask[x, y] = true;

                    if (map.GetCell[x, y].IsPassable())
                    {
                        CheckCells(x - 1, y, checkMask); //check above
                        CheckCells(x + 1, y, checkMask); //check under
                        CheckCells(x, y - 1, checkMask); //check left
                        CheckCells(x, y + 1, checkMask); //check right
                    }
                }
            }
    
            bool[,] accessibleCells = new bool[map.GetCell.GetLength(0), map.GetCell.GetLength(0)];

            //Initial MapCell for check
            Random random = new Random();
            int x;
            int y;

            do
            {
            x = random.Next(map.GetCell.GetLength(0));
            y = random.Next(map.GetCell.GetLength(0));
            }
            while (!map.GetCell[x, y].IsPassable());

            //Check map
            CheckCells(x, y, accessibleCells);

            //Check accesibleCells
            bool check = true;

            for (int i = 1; i < map.GetCell.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < map.GetCell.GetLength(0) - 1; j++)
                {
                    if (!accessibleCells[i, j])
                        check = false;
                }
            }

            return check;
        }

        private bool VerifyHeroPlacement(Map map)
        {
            bool check = true;

            int dimension = map.GetCell.GetLength(0);
            List<Vector2> heroesPositions = new List<Vector2>{new Vector2(1, 1), new Vector2(dimension - 2, dimension - 2),
                                        new Vector2(1, dimension - 2), new Vector2(dimension - 2, 1)};  

            foreach (MapCell cell in map.GetCell)
            {
                if (cell.GetOccupant is Hero)
                    check = heroesPositions.Remove(cell.GetOccupant.Position);
            }

            return check;      
        }

        private bool VerifyHeroOnBlockedCell(Map map)
        {
            bool check = true;

            foreach (MapCell cell in map.GetCell)
            {
                if (cell.GetOccupant is Hero)
                    if (!cell.IsPassable())
                    {
                        check = false;
                        break;
                    }
            }

            return check;
        }
    }
}



//TestGenerateGems_AddsGemsToMap
//TestGenerateTraps_AddsTrapsToMap