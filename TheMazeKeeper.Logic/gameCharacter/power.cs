namespace TheMazeKeeper.Logic.GameCharacter
{
    class Power
    {
        string name;

        string effect;
        int energyCost;

        int cooldown;
        int currentCooldown = 0;

        public Power(string name)
        {
            // Dictionary<name, (effect, cooldown, energyCost)>
            var PowersBase = new Dictionary<string, (string, int, int)>
            {
                {"Adrenaline Rush", ("Adrenaline Boost", 8, 3)},
                {"Swift Reflexes", ("Reflexive Frenzy", 3, 1)},
                {"Quick Surge", ("Surge of Momentum", 5, 2)},
            };

            this.name = name;

            var attributes = PowersBase[name];
            effect = attributes.Item1;
            cooldown = attributes.Item2;
            energyCost = attributes.Item3;
        }

        public string Activate(int CurrentTurn)
        {
            currentCooldown = CurrentTurn + cooldown;

            return effect;
        }

        public int GetCooldown { get => currentCooldown; }

        public int GetEnergyCost { get => energyCost; }

        public string GetName { get => name; }
    }
}