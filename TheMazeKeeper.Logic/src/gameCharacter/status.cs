namespace TheMazeKeeper.Logic.GameCharacter
{
    class Status
    {
        string name;

        int effectsEnergy;
        int effectsIniciative;
        int duration;
        int currentDuration;

        //name, (duration, energy, iniciative)
        public Status(string name, int CurrentTurn)
        {
            currentDuration = duration + CurrentTurn;

            Dictionary<string, (int, int, int)> statesBase = new Dictionary<string, (int, int, int)>
            {
                {"Adrenaline Boost", (3, 3, 0)},
                {"Reflexive Frenzy", (2, 0, 3)},
                {"Surge of Momentum", (3, 1, 2)},
                {"Drained", (3, -2, 0)},
                {"Slowed", (2, 0, -3)},
            };

            this.name = name;
            
            var status = statesBase[name];
            duration = status.Item1;
            effectsEnergy = status.Item2;
            effectsIniciative = status.Item3;
        }

        public int[] Effect() 
        { 
            return new int[] {effectsEnergy, effectsIniciative}; 
        }

        public void RebootDuration(int currentTurn)
        {
            currentDuration = currentTurn + duration;
        }

        public string Name { get => name; }
        public int Duration { get => currentDuration; }
    }
}