using TheMazeKeeper.Logic.GameElement;
using TheMazeKeeper.Logic.MapStructure;

namespace TheMazeKeeper.MapTests
{
    public class MapGeneratorTests
    {
        [Fact]
        public void TestGenerateMap_CreatesValidMap()
        {
            Map testMap = new Map(50, 4);

            Assert.True(VerifyMapBordersAreWalls(testMap));
            Assert.True(VerifyMapHasAtLeastOneObstacle(testMap));
            Assert.True(VerifyMapIsAccessible(testMap));
        }

        private bool VerifyMapBordersAreWalls(Map map)
        {
            bool check = true;

            bool IsWall(int x, int y)
            {
                bool check = true;

                if (map.GetCell[x, y].GetElement.Getname != "mapWall")
                    check = false;

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
    }
}



//TestGenerateGems_AddsGemsToMap
//TestGenerateTraps_AddsTrapsToMap