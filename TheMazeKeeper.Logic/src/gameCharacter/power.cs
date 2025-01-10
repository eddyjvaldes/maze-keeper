namespace TheMazeKeeper.Logic.GameCharacter
{
    class Power
    {
        string name;
        string effect;
        int cooldown;
        int currentCooldown = 0;
        int energyCost;

        //name, (effect, cooldown, energyCost)
        public Power(string name)
        {
            Dictionary<string, (string, int, int)> PowersBase = new Dictionary<string, (string, int, int)>
            {
                {"Adrenaline Rush", ("Adrenaline Boost", 8, 4)},
                {"Swift Reflexes", ("Reflexive Frenzy", 3, 3)},
                {"Quick Surge", ("Surge of Momentum", 5, 3)},
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

        public int Cooldown { get => currentCooldown; }
        public int EnergyCost { get => energyCost; }
    }
}