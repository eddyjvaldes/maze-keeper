using System.Numerics;
using TheMazeKeeper.Logic.GameElement;
using TheMazeKeeper.Logic.GameManagement;
using TheMazeKeeper.Logic.MapStructure;

namespace TheMazeKeeper.Logic.GameCharacter
{
    class Hero
    {
        Player player;
        string name;
        Vector2 position;
        int maxEnergy;
        int currentEnergy;
        int iniciative;
        int currentIniciative;
        Power power;
        List<Status> listStates = new List<Status>();
        
        //name, (maxEnegy, iniciative, Power)
        public Hero(Player player, string name, int x, int y)
        {
            this.player = player;
            position = new Vector2(x, y);

            Dictionary<string, (int, int, string)> herosBase = new Dictionary<string, (int, int, string)>
            {
                {"Titania the Swift", (3, 5, "Swift Reflexes")},
                {"Baron of Brawn", (5, 3, "Adrenaline Rush")},
                {"Blaze the Quick", (4, 4, "Quick Surge")},
                {"Vanguard the Titan", (5, 2, "Adrenaline Rush")},
                {"Phantom the Silent", (3, 5, "Swift Reflexes")},
            };

            this.name = name;
            
            var attributes = herosBase[name];
            maxEnergy = attributes.Item1;
            iniciative = attributes.Item2;
            power = new Power(attributes.Item3);
        }

        //Debido a los bordes exteriores del mapa no es necesario revisar el rango
        public void MoveDirection(Vector2 direction, MapCell[,] map, int currentTurn)
        {
                Vector2 newPosition = position + direction;
                MapCell newCell = map[(int)newPosition.X, (int)newPosition.Y];

                if (newCell.IsAccessible()) 
                {
                   newCell.AddOccupant(this);
                   map[(int)position.X, (int)position.Y].RemoveOccupant();
                   position = newPosition;

                   currentEnergy--;

                   if (newCell.HasElement())
                   {
                        if (newCell.GetElement is Gem)
                        {
                            Gem gem = (Gem)newCell.GetElement;
                            player.AddScore(gem.GetPoints);
                        }   
                        else if (newCell.GetElement is Tramp)
                        {
                            Tramp tramp = (Tramp)newCell.GetElement;
                            ApplyNewStatus(tramp.Effect, currentEnergy);
                        }

                        newCell.RemoveElement();
                   }
                }
        }

        public void UsePower(int currentTurn)
        {
            if (currentEnergy - power.EnergyCost >= 0 && power.Cooldown <= currentTurn)
            {
                currentEnergy-= power.EnergyCost;
                ApplyNewStatus(power.Activate(currentTurn), currentTurn);
            }
        }

        public void HeroRecovery()
        {
            currentEnergy = maxEnergy;
            currentIniciative = iniciative;
        }

        public void UpdateStatusEffects(int currentTurn)
        {
            bool check = false;
 
            while(!check)
            {
                check = true;

                for (int i = 0; i < listStates.Count; i++)
                {
                    if (listStates[i].Duration < currentTurn)
                    {
                        check = false;
                        listStates.RemoveAt(i);
                        break;
                    }
                }
            }

            for (int i = 0; i < listStates.Count; i++)
                ApplyStatusEffects(listStates[i]);
        }

        void ApplyStatusEffects(Status status)
        {
            int[] effects = status.Effect();
                
            currentEnergy+= effects[0];
            currentIniciative+= effects[1];
        }

        void ApplyNewStatus(string statusName, int currentTurn)
        {
            bool check = true;

            for (int i = 0; i < listStates.Count; i++)
            {
                if (listStates[i].Name == statusName)
                {
                    check = false;
                    listStates[i].RebootDuration(currentTurn);
                }
            }

            if (check)
            {
                listStates.Add(new Status(statusName, currentTurn));
                ApplyStatusEffects(listStates[listStates.Count - 1]);
            }
        }

        public string Name { get => name; }

        public Vector2 Position { get => position; }

        public int GetEnergy { get => currentEnergy; }

        public int GetIniciative { get => currentIniciative; }

        public Power GetPower { get => power; }

        public List<Status> GetListStates { get => listStates; }

        public Player GetPlayer { get => player; }
    }
}