namespace TheMazeKeeper.Logic.GameElement
{
    abstract class Interactive : Element
    {
        protected int duration;

        public int Duration { get => duration; }
    }
}