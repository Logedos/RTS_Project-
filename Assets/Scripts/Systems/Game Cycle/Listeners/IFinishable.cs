namespace Systems.GameCycle
{
    public interface IFinishable : IGameListener
    {
        void OnFinish();
    }
}