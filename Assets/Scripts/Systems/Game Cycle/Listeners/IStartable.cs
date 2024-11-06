namespace Systems.GameCycle
{
    public interface IStartable : IGameListener
    {
        void OnStart();
    }
}