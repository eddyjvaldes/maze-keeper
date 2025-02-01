using TheMazeKeeper.Logic.GameElement;
using TheMazeKeeper.Logic.GameManagement;
using TheMazeKeeper.Logic.MapStructure;

namespace TheMazeKeeper.Logic.GameCharacter
{
    class Hero
    {
        Player player;
        string name;

        int x;
        int y;

        int maxEnergy;
        int currentEnergy;

        int initiative;
        int currentInitiative;

        Power power;
        List<Status> activeStates = new();

        public Hero(Player player, string name, int x, int y)
        {
            this.player = player;
            this.name = name;

            this.x = x;
            this.y = y;

            // Dictionary<name, (maxEnergy, initiative, Power)>
            var heroesBase = new Dictionary<string, (int, int, string)>
            {
                {"Titania the Swift", (3, 5, "Swift Reflexes")},
                {"Baron of Brawn", (5, 3, "Adrenaline Rush")},
                {"Blaze the Quick", (4, 4, "Quick Surge")},
                {"Vanguard the Titan", (5, 2, "Adrenaline Rush")},
                {"Phantom the Silent", (3, 5, "Swift Reflexes")},
            };

            var attributes = heroesBase[name];
            maxEnergy = attributes.Item1;
            initiative = attributes.Item2;

            power = new Power(attributes.Item3);
        }

        public bool Move(int x, int y, MapCell[,] map, int currentTurn)
        {
            var newCell = map[this.x + x, this.y + y];

            if (newCell.IsAccessible())
            {
                map[this.x, this.y].RemoveOccupant();

                newCell.PlaceOccupant(this);
                this.x += x;
                this.y += y;

                currentEnergy--;

                ActivateCellElement(map, currentTurn);

                return true;
            }

            return false;
        }

        public void ActivateCellElement(MapCell[,] map, int currentTurn)
        {
            var currentCell = map[x, y];

            Element? element = currentCell.GetElement;

            if (element != null)
            {
                if (element is Gem)
                {
                    var gem = (Gem)element;
                    player.AddScore(gem.GetPoints);
                }
                else if (element is Tramp)
                {
                    var tramp = (Tramp)element;
                    ApplyNewStatus(tramp.GetEffect, currentTurn);
                }

                currentCell.RemoveElement();
            }
        }

        public bool UsePower(int currentTurn)
        {
            if (currentEnergy - power.GetEnergyCost >= 0 && power.GetCooldown <= currentTurn)
            {
                currentEnergy -= power.GetEnergyCost;

                ApplyNewStatus(power.Activate(currentTurn), currentTurn);

                return true;
            }

            return false;
        }

        public void HeroRecovery()
        {
            currentEnergy = maxEnergy;
            currentInitiative = initiative;
        }

        public void UpdateStatus(int currentTurn)
        {
            RemoveInactiveStates(currentTurn);

            foreach (var status in activeStates)
            {
                ApplyStatusEffects(status);
            }
        }

        void RemoveInactiveStates(int currentTurn)
        {
            bool inactiveStatesRemoved;

            do
            {
                inactiveStatesRemoved = true;

                for (int i = 0; i < activeStates.Count; i++)
                {
                    if (activeStates[i].GetDuration <= currentTurn)
                    {
                        inactiveStatesRemoved = false;
                        activeStates.RemoveAt(i);

                        break;
                    }
                }
            }
            while (!inactiveStatesRemoved);
        }

        void ApplyStatusEffects(Status status)
        {
            var effects = status.GetEffect();

            currentEnergy += effects.Item1;
            currentInitiative += effects.Item2;
        }

        void ApplyNewStatus(string statusName, int currentTurn)
        {
            bool newStatus = true;

            foreach (var status in activeStates)
            {
                if (status.GetName == statusName)
                {
                    newStatus = false;
                    status.RebootDuration(currentTurn);

                    break;
                }
            }

            if (newStatus)
            {
                var status = new Status(statusName, currentTurn);

                activeStates.Add(status);
                ApplyStatusEffects(status);
            }
        }

        public string GetName { get => name; }

        public (int, int) GetPosition()
        {
            return (x, y);
        }

        public int GetEnergy { get => currentEnergy; }

        public int GetInitiative { get => currentInitiative; }

        public Power GetPower { get => power; }

        public Player GetPlayer { get => player; }

        public List<Status> GetActiveStatesList { get => activeStates; }
    }
}