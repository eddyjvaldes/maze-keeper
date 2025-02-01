namespace TheMazeKeeper.Logic.GameCharacter
{
    class Status
    {
        string name;

        int effectsEnergy;
        int effectsInitiative;

        int duration;
        int currentDuration;

        public Status(string name, int currentTurn)
        {
            this.name = name;

            // Dictionary<name, (duration, energy, initiative)>
            var statesBase = new Dictionary<string, (int, int, int)>
            {
                {"Adrenaline Boost", (3, 3, 0)},
                {"Reflexive Frenzy", (2, 0, 3)},
                {"Surge of Momentum", (3, 1, 2)},
                {"Drained", (3, -2, 0)},
                {"Slowed", (2, 0, -3)},
            };


            var status = statesBase[name];
            duration = status.Item1;
            effectsEnergy = status.Item2;
            effectsInitiative = status.Item3;

            RebootDuration(currentTurn);
        }

        public (int, int) GetEffect()
        {
            return (effectsEnergy, effectsInitiative);
        }

        public void RebootDuration(int currentTurn)
        {
            currentDuration = currentTurn + duration;
        }

        public string GetName { get => name; }

        public int GetDuration { get => currentDuration; }
    }
}